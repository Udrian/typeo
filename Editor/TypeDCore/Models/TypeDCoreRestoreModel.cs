using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
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

            RestoreModel.AddRestoreMethod(Restore);
        }

        // Functions
        public void Restore(Project project)
        {
            if (project.Assembly == null) return;
            var types = project.Assembly.GetTypes();

            foreach(var type in types)
            {
                Type foundType = null;
                if(type.IsSubclassOf(typeof(Entity)))
                {
                    foundType = typeof(Entity);
                }
                else if(type.IsSubclassOf(typeof(Scene)))
                {
                    foundType = typeof(Scene);
                }
                else if (type.IsSubclassOf(typeof(Drawable2d)))
                {
                    foundType = typeof(Drawable2d);
                }

                if(foundType != null && !ComponentProvider.Exists(project, type))
                {
                    if(foundType == typeof(Entity))
                    {
                        var updatable = type.GetInterfaces().Contains(typeof(IUpdatable));
                        var drawable = type.GetInterfaces().Contains(typeof(IDrawable));
                        TypeDCoreProjectModel.CreateEntity(project, type.Name, type.Namespace, type.BaseType.FullName, updatable, drawable);
                    }
                    else if(foundType == typeof(Scene))
                    {
                        TypeDCoreProjectModel.CreateScene(project, type.Name, type.Namespace, type.BaseType.FullName);
                    }
                    else if (foundType == typeof(Drawable2d))
                    {
                        TypeDCoreProjectModel.CreateDrawable2d(project, type.Name, type.Namespace, type.BaseType.FullName);
                    }

                    var csFilePath = Path.Combine(project.Location, $"{type.FullName.Replace('.', Path.DirectorySeparatorChar)}.cs");
                    if(File.Exists(csFilePath))
                    {
                        var fileContent = File.ReadAllText(csFilePath);
                        bool needToSave = false;

                        ReplaceCode(ref fileContent, ref needToSave, $"class {type.Name}", $"partial class {type.Name}");
                        if (foundType == typeof(Entity) || foundType == typeof(Scene))
                        {
                            ReplaceCode(ref fileContent, ref needToSave, $"public override void Initialize()", $"protected void InternalInitialize()");
                        }

                        if (needToSave)
                        {
                            var codes = SaveModel.GetSaveContext<Dictionary<string, string>>("TypeDCoreRestoreCode") ?? new Dictionary<string, string>();
                            codes.Add(csFilePath, fileContent);
                            SaveModel.AddSave("TypeDCoreRestoreCode", codes,
                                (context) => {
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
