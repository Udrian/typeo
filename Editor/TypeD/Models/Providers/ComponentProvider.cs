using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

            return new Component()
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
        }

        public void Save(Project project, Component component)
        {
            var components = SaveModel.GetSaveContext<List<Component>>("Components") ?? new List<Component>();
            components.Add(component);

            SaveModel.AddSave("Components", components, (context) =>
            {
                return Task.Run(() => {
                    var saveComponents = context as List<Component>;
                    foreach(var saveComponent in saveComponents)
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

        public void Delete(Project project, Component component)
        {
            var components = SaveModel.GetSaveContext<List<Component>>("Components") ?? new List<Component>();
            components.Remove(component);
            var delComponents = SaveModel.GetSaveContext<List<Component>>("Deleted_Components") ?? new List<Component>();
            delComponents.Add(component);

            SaveModel.AddSave("Deleted_Components", delComponents, (context) =>
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
            var delComponents = SaveModel.GetSaveContext<List<Component>>("Deleted_Components") ?? new List<Component>();

            var retList = components.Union(unsavedComponents)
                                    .GroupBy(t => t.FullName)
                                    .Select(t => t.First())
                                    .ToList();
            retList.RemoveAll(c => delComponents.Exists(d => c.FullName == d.FullName));
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
