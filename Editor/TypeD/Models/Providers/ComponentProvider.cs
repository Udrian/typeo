using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.DTO;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

namespace TypeD.Models.Providers
{
    public class ComponentProvider : IComponentProvider, IProvider
    {
        private static string ComponentFileEnding = "component";

        // Model
        IResourceModel ResourceModel { get; set; }
        IProjectModel ProjectModel { get; set; }
        ISaveModel SaveModel { get; set; }

        // Constructors
        public ComponentProvider()
        {
        }

        public void Init(IResourceModel resourceModel)
        {
            ResourceModel = resourceModel;

            ProjectModel = ResourceModel.Get<IProjectModel>();
            SaveModel = ResourceModel.Get<ISaveModel>();
        }

        // Functions
        public void Create<T>(Project project, string className, string @namespace, Component parentComponent = null, List<string> interfaces = null) where T : ComponentTypeCode
        {
            Type codeType = typeof(T);

            var component = TranslateComponentDTO(project, new ComponentDTO()
            {
                ClassName = className,
                Interfaces = interfaces ?? new List<string>(),
                Namespace = @namespace,
                ParentComponent = parentComponent?.FullName ?? "",
                TemplateClass = codeType.FullName
            });
            component.ParentComponent = parentComponent;

            var code = Activator.CreateInstance(codeType, component) as T;
            component.TypeOBaseType = code.TypeOBaseType;
            ProjectModel.InitAndSaveCode(project, code);
            component.Code = code;

            Save(project, component);

            ProjectModel.BuildComponentTree(project);
        }

        public void Save(Project project, Component component)
        {
            var components = SaveModel.GetSaveContext<List<Component>>("Components") ?? new List<Component>();
            components.Add(component);

            SaveModel.AddSave("Components", components, (context) =>
            {
                return Task.Run(() => {
                    var saveComponents = context as List<Component>;
                    foreach (var saveComponent in saveComponents)
                    {
                        JSON.Serialize(new ComponentDTO()
                        {
                            ClassName = saveComponent.ClassName,
                            Interfaces = saveComponent.Interfaces.Select(i => i.FullName).ToList(),
                            Namespace = saveComponent.Namespace,
                            ParentComponent = saveComponent.ParentComponent?.FullName ?? "",
                            TemplateClass = saveComponent.TemplateClass.FullName,
                            TypeOBaseType = saveComponent.TypeOBaseType.FullName
                        }, GetPath(project, saveComponent.FullName));
                    }
                    if (!project.IsClosing)
                        ProjectModel.BuildComponentTree(project);
                });
            });
        }

        public Component Load(Project project, string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return null;
            var path = GetPath(project, fullName);

            return LoadFromPath(project, path);
        }

        private Component LoadFromPath(Project project, string path)
        {
            if (!File.Exists(path)) return null;
            var dto = JSON.Deserialize<ComponentDTO>(path);

            var component = TranslateComponentDTO(project, dto);

            var code = Activator.CreateInstance(component.TemplateClass, component) as ComponentTypeCode;
            ProjectModel.InitCode(project, code);
            component.Code = code;

            return component;
        }

        private Component TranslateComponentDTO(Project project, ComponentDTO dto)
        {
            var component = new Component()
            {
                ClassName = dto.ClassName,
                Interfaces = dto.Interfaces.Select(
                                i => AppDomain.CurrentDomain.GetAssemblies()
                                    .SelectMany(a => a.GetTypes())
                                    .FirstOrDefault(t => t.FullName.Equals(i))).ToList(),
                Namespace = dto.Namespace,
                ParentComponent = Load(project, dto.ParentComponent),
                TemplateClass = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .FirstOrDefault(t => t.FullName.Equals(dto.TemplateClass)),
                TypeOBaseType = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .FirstOrDefault(t => t.FullName.Equals(dto.TypeOBaseType))
            };

            return component;
        }

        public void Delete(Project project, Component component)
        {
            var components = SaveModel.GetSaveContext<List<Component>>("Components") ?? new List<Component>();
            components.Remove(component);
            var delComponents = SaveModel.GetSaveContext<List<Component>>("deleted_components") ?? new List<Component>();
            delComponents.Add(component);

            SaveModel.AddSave("deleted_components", delComponents, (context) =>
            {
                return Task.Run(() =>
                {
                    var deleteComponents = context as List<Component>;
                    foreach (var delComponent in deleteComponents)
                    {
                        if (Exists(project, delComponent))
                        {
                            File.Delete(GetPath(project, delComponent.FullName));
                            var csFile = Path.Combine(project.Location, $"{delComponent.FullName.Replace('.', Path.DirectorySeparatorChar)}.cs");
                            var csTypeDFile = Path.Combine(project.Location, $"{delComponent.FullName.Replace('.', Path.DirectorySeparatorChar)}.typed.cs");
                            if (File.Exists(csFile))
                                File.Delete(csFile);
                            if (File.Exists(csTypeDFile))
                                File.Delete(csTypeDFile);
                        }
                    }
                });
            });

            ProjectModel.BuildComponentTree(project);
        }

        public void Rename(Project project, Component component, string newClassName)
        {
            var oldClassname = component.ClassName;
            component.ClassName = newClassName;

            var renameComponents = SaveModel.GetSaveContext<List<Tuple<string, Component>>>("renamed_components") ?? new List<Tuple<string, Component>>();
            renameComponents.Add(new Tuple<string, Component>(oldClassname, component));

            SaveModel.AddSave("renamed_components", renameComponents, (context) =>
            {
                return Task.Run(() =>
                {
                    var renameComponents = context as List<Tuple<string, Component>>;
                    foreach (var renameComponent in renameComponents)
                    {
                        var oldFullName = $"{renameComponent.Item2.Namespace}.{renameComponent.Item1}";

                        File.Delete(GetPath(project, oldFullName));
                        var csFile = Path.Combine(project.Location, $"{oldFullName.Replace('.', Path.DirectorySeparatorChar)}.cs");
                        var csTypeDFile = Path.Combine(project.Location, $"{oldFullName.Replace('.', Path.DirectorySeparatorChar)}.typed.cs");
                        var csFileNew = Path.Combine(project.Location, $"{renameComponent.Item2.FullName.Replace('.', Path.DirectorySeparatorChar)}.cs");
                        var csTypeDFileNew = Path.Combine(project.Location, $"{renameComponent.Item2.FullName.Replace('.', Path.DirectorySeparatorChar)}.typed.cs");
                        if (File.Exists(csFile))
                        {
                            File.Move(csFile, csFileNew);
                            File.WriteAllText(csFileNew, File.ReadAllText(csFileNew).Replace($"class {renameComponent.Item1}", $"class {renameComponent.Item2.ClassName}"));
                        }
                        if (File.Exists(csTypeDFile))
                        {
                            File.Move(csTypeDFile, csTypeDFileNew);
                            File.WriteAllText(csTypeDFileNew, File.ReadAllText(csTypeDFileNew).Replace($"class {renameComponent.Item1}", $"class {renameComponent.Item2.ClassName}"));
                        }
                    }
                });
            });

            Save(project, component);

            ProjectModel.BuildComponentTree(project);
        }

        public bool Exists(Project project, Component component)
        {
            return File.Exists(GetPath(project, component.FullName));
        }

        public bool Exists(Project project, Type type)
        {
            return File.Exists(GetPath(project, type.FullName));
        }

        public List<Component> ListAll(Project project)
        {
            var path = GetPath(project);
            if (!Directory.Exists(path)) return new List<Component>();

            var files = Directory.GetFiles(path, $"*.{ComponentFileEnding}", SearchOption.AllDirectories);
            var components = files.Select((f) => { return LoadFromPath(project, f); }).ToList();

            var unsavedComponents = SaveModel.GetSaveContext<List<Component>>("Components") ?? new List<Component>();
            var delComponents = SaveModel.GetSaveContext<List<Component>>("deleted_components") ?? new List<Component>();
            var renamedComponents = SaveModel.GetSaveContext<List<Tuple<string, Component>>>("renamed_components") ?? new List<Tuple<string, Component>>();
            
            var retList = components.Union(unsavedComponents)
                                    .GroupBy(t => t.FullName)
                                    .Select(t => t.First())
                                    .ToList();
            retList.RemoveAll(c => delComponents.Exists(d => c.FullName == d.FullName));
            retList.RemoveAll(c => renamedComponents.Exists(r => c.FullName == $"{r.Item2.Namespace}.{r.Item1}"));
            return retList;
        }

        // Internal
        private string GetPath(Project project, string fullName)
        {
            return Path.Combine(GetPath(project), $"{fullName.Replace('.', Path.DirectorySeparatorChar)}.{ComponentFileEnding}");
        }

        private string GetPath(Project project)
        {
            return Path.Combine(project.ProjectTypeOPath, "components");
        }
    }
}
