using System.Collections.Generic;
using TypeD;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDCore.Code.Drawable;
using TypeDCore.Code.Entity;
using TypeDCore.Code.Scene;
using TypeDCore.Models.Interfaces;

namespace TypeDCore.Models
{
    class TypeDCoreProjectModel : ITypeDCoreProjectModel, IModel
    {
        // Models
        IProjectModel ProjectModel { get; set; }
        ISaveModel SaveModel { get; set; }

        // Providers
        IProjectProvider ProjectProvider { get; set; }
        IComponentProvider ComponentProvider { get; set; }

        // Constructors
        public TypeDCoreProjectModel()
        {
        }

        public void Init(IResourceModel resourceModel)
        {
            ProjectModel = resourceModel.Get<IProjectModel>();
            SaveModel = resourceModel.Get<ISaveModel>(); ;
            ProjectProvider = resourceModel.Get<IProjectProvider>(); ;
            ComponentProvider = resourceModel.Get<IComponentProvider>(); ;
        }

        // Functions
        public void CreateEntity(Project project, string className, string @namespace, string baseClass, bool updatable, bool drawable)
        {
            @namespace = (@namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}").Replace("\\", ".").Replace("/", ".");
            var entityCode = new EntityCode(className, @namespace, baseClass, updatable, drawable);

            ProjectModel.AddCode(project, entityCode);
            ProjectModel.AddCode(project, new EntityTypeDCode(className, @namespace, baseClass));
            ComponentProvider.Save(project, new Component()
            {
                ClassName = entityCode.ClassName,
                Namespace = entityCode.Namespace,
                TemplateClass = new List<string>()
                {
                    typeof(EntityCode).FullName,
                    typeof(EntityTypeDCode).FullName
                },
                TypeOBaseType = "Entity2d"
            });

            ProjectModel.BuildComponentTree(project);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }

        public void CreateScene(Project project, string className, string @namespace, string baseClass)
        {
            @namespace = (@namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}").Replace("\\", ".").Replace("/", ".");
            var sceneCode = new SceneCode(className, @namespace, baseClass);
            ProjectModel.AddCode(project, sceneCode);
            ProjectModel.AddCode(project, new SceneTypeDCode(className, @namespace, baseClass));
            ComponentProvider.Save(project, new Component()
            {
                ClassName = sceneCode.ClassName,
                Namespace = sceneCode.Namespace,
                TemplateClass = new List<string>()
                {
                    typeof(SceneCode).FullName,
                    typeof(SceneTypeDCode).FullName
                },
                TypeOBaseType = "Scene"
            });

            ProjectModel.BuildComponentTree(project);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }

        public void CreateDrawable2d(Project project, string className, string @namespace, string baseClass)
        {
            @namespace = (@namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}").Replace("\\", ".").Replace("/", ".");
            var drawable2dCode = new Drawable2dCode(className, @namespace, baseClass);
            ProjectModel.AddCode(project, drawable2dCode);
            ComponentProvider.Save(project, new Component()
            {
                ClassName = drawable2dCode.ClassName,
                Namespace = drawable2dCode.Namespace,
                TemplateClass = new List<string>()
                {
                    typeof(Drawable2dCode).FullName
                },
                TypeOBaseType = "Drawable2d"
            });

            ProjectModel.BuildComponentTree(project);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }
    }
}
