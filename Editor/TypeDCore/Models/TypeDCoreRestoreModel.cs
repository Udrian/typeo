using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDCore.Code.Drawable;
using TypeDCore.Code.Entity;
using TypeDCore.Code.Game;
using TypeDCore.Code.Scene;
using TypeDCore.Models.Interfaces;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeDCore.Models
{
    public class TypeDCoreRestoreModel : ITypeDCoreRestoreModel, IModel
    {
        // Models
        IRestoreModel RestoreModel { get; set; }
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }
        IComponentProvider ComponentProvider { get; set; }
        ISaveModel SaveModel { get; set; }
        IProjectModel ProjectModel { get; set; }

        // Constructors
        public TypeDCoreRestoreModel()
        {

        }

        public void Init(IResourceModel resourceModel)
        {
            RestoreModel = resourceModel.Get<IRestoreModel>();
            TypeDCoreProjectModel = resourceModel.Get<ITypeDCoreProjectModel>();
            ComponentProvider = resourceModel.Get<IComponentProvider>();
            SaveModel = resourceModel.Get<ISaveModel>();
            ProjectModel = resourceModel.Get<IProjectModel>();

            RestoreModel.AddRestoreMethod(Restore);
        }

        // Functions
        public void Restore(Project project)
        {
            var types = project.Assembly?.GetTypes().ToList() ?? new List<Type>();

            // Check if we have types that are missing .component file
            foreach (var type in types)
            {
                // Only restore if type is a subclass to any of the following types
                if (new List<Type>() { typeof(Entity), typeof(Scene), typeof(Drawable2d) }.Find(t => type.IsSubclassOf(t)) == null)
                {
                    continue;
                }

                // True if .component files are missing, we need to restore them
                if (!ComponentProvider.Exists(project, type))
                {
                    // Fetch parent component, will be null if there is none
                    var parentComponent = ComponentProvider.Load(project, type.BaseType.FullName);

                    if (type == typeof(Entity))
                    {
                        var updatable = type.GetInterfaces().Contains(typeof(IUpdatable));
                        var drawable = type.GetInterfaces().Contains(typeof(IDrawable));
                        TypeDCoreProjectModel.CreateEntity(project, type.Name, type.Namespace, parentComponent, updatable, drawable);
                    }
                    else if (type == typeof(Scene))
                    {
                        TypeDCoreProjectModel.CreateScene(project, type.Name, type.Namespace, parentComponent);
                    }
                    else if (type == typeof(Drawable2d))
                    {
                        TypeDCoreProjectModel.CreateDrawable2d(project, type.Name, type.Namespace, parentComponent);
                    }

                    var csFilePath = Path.Combine(project.Location, $"{type.FullName.Replace('.', Path.DirectorySeparatorChar)}.cs");
                    // If CS files already exists, check if we need to convert them to typed.cs pair
                    if (File.Exists(csFilePath))
                    {
                        var fileContent = File.ReadAllText(csFilePath);
                        bool needToSave = false;

                        ReplaceCode(ref fileContent, ref needToSave, $"class {type.Name}", $"partial class {type.Name}");
                        if (type == typeof(Entity) || type == typeof(Scene))
                        {
                            ReplaceCode(ref fileContent, ref needToSave, $"public override void Initialize()", $"protected void InternalInitialize()");
                        }

                        if (needToSave)
                        {
                            var codes = SaveModel.GetSaveContext<Dictionary<string, string>>("TypeDCoreRestoreCode") ?? new Dictionary<string, string>();
                            codes.Add(csFilePath, fileContent);
                            SaveModel.AddSave("TypeDCoreRestoreCode", codes,
                                (context) =>
                                {
                                    return Task.Run(() =>
                                    {
                                        foreach (var saveCode in context as Dictionary<string, string>)
                                        {
                                            File.WriteAllText(saveCode.Key, saveCode.Value);
                                        }
                                    });
                                });
                        }
                    }
                }
            }

            // Check if we are missing Program.cs and Game.cs
            if(!File.Exists(Path.Combine(project.Location, project.ProjectName, "Program.cs")))
            {
                ProjectModel.InitAndSaveCode(project, new ProgramCode());
            }

            bool updateTree = false;
            // Check if we have components that are missing CS files
            foreach (var component in ComponentProvider.ListAll(project))
            {
                var csFile = Path.Combine(project.Location, $"{component.FullName.Replace('.', Path.DirectorySeparatorChar)}.cs");
                var csTypeDFile = Path.Combine(project.Location, $"{component.FullName.Replace('.', Path.DirectorySeparatorChar)}.typed.cs");
                var parentComponent = ComponentProvider.Load(project, component.ParentComponent?.FullName);

                if (component.TypeOBaseType == typeof(Entity2d))
                {
                    if (!File.Exists(csFile) || !File.Exists(csTypeDFile))
                    {
                        var updatable = component.Interfaces.Contains(typeof(IUpdatable));
                        var drawable = component.Interfaces.Contains(typeof(IDrawable));
                        ProjectModel.InitAndSaveCode(project, new EntityCode(component));
                        updateTree = true;
                    }
                }
                else if (component.TypeOBaseType == typeof(Scene))
                {
                    if (!File.Exists(csFile) || !File.Exists(csTypeDFile))
                    {
                        ProjectModel.InitAndSaveCode(project, new SceneCode(component));
                        updateTree = true;
                    }
                }
                else if (component.TypeOBaseType == typeof(Drawable2d))
                {
                    if (!File.Exists(csFile))
                    {
                        ProjectModel.InitAndSaveCode(project, new Drawable2dCode(component));
                        updateTree = true;
                    }
                }
                else if(component.TypeOBaseType == typeof(Game))
                {
                    if (!File.Exists(Path.Combine(project.Location, project.ProjectName, $"{project.ProjectName}Game.cs")))
                    {
                        ProjectModel.InitAndSaveCode(project, new GameCode(component));
                    }
                }
            }

            if(updateTree)
            {
                ProjectModel.BuildComponentTree(project);
            }
        }

        private void ReplaceCode(ref string code, ref bool needToSave, string search, string replace)
        {
            if (!code.Contains(replace))
            {
                needToSave = true;
                code = code.Replace(search, replace);
            }
        }
    }
}
