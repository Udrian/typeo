using System.Collections.Generic;
using System.Linq;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDCore.Code.Drawable;
using TypeDCore.Code.Entity;
using TypeDCore.Code.Scene;
using TypeDCore.Models.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

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
        public void CreateEntity(Project project, string className, string @namespace, Component parentComponent, bool updatable, bool drawable)
        {
            @namespace = (@namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}").Replace("\\", ".").Replace("/", ".");
            var entityCode = new EntityCode(className, @namespace, parentComponent, updatable, drawable);
            var entityTypeDCode = new EntityTypeDCode(className, @namespace, parentComponent, updatable, drawable);

            var interfaces = entityCode.GetInterfaces().Select((i) => { return i.FullName; })
                             .Union(entityTypeDCode.GetInterfaces().Select((i) => { return i.FullName; })).ToList();
            if(updatable && !interfaces.Contains(typeof(IUpdatable).FullName))
            {
                interfaces.Add(typeof(IUpdatable).FullName);
            }
            if (drawable && !interfaces.Contains(typeof(IDrawable).FullName))
            {
                interfaces.Add(typeof(IDrawable).FullName);
            }

            ProjectModel.AddCode(project, entityCode);
            ProjectModel.AddCode(project, entityTypeDCode);
            ComponentProvider.Save(project, new Component()
            {
                ClassName = entityCode.ClassName,
                Namespace = entityCode.Namespace,
                ParentComponent = parentComponent == null ? "" : parentComponent.FullName,
                Interfaces = interfaces,
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

        public void CreateScene(Project project, string className, string @namespace, Component parentComponent)
        {
            @namespace = (@namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}").Replace("\\", ".").Replace("/", ".");
            var sceneCode = new SceneCode(className, @namespace, parentComponent);
            ProjectModel.AddCode(project, sceneCode);
            ProjectModel.AddCode(project, new SceneTypeDCode(className, @namespace, parentComponent));
            ComponentProvider.Save(project, new Component()
            {
                ClassName = sceneCode.ClassName,
                Namespace = sceneCode.Namespace,
                ParentComponent = parentComponent == null ? "" : parentComponent.FullName,
                Interfaces = sceneCode.GetInterfaces().Select((i) => { return i.FullName; }).ToList(),
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

        public void CreateDrawable2d(Project project, string className, string @namespace, Component parentComponent)
        {
            @namespace = (@namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}").Replace("\\", ".").Replace("/", ".");
            var drawable2dCode = new Drawable2dCode(className, @namespace, parentComponent);
            ProjectModel.AddCode(project, drawable2dCode);
            ComponentProvider.Save(project, new Component()
            {
                ClassName = drawable2dCode.ClassName,
                Namespace = drawable2dCode.Namespace,
                ParentComponent = parentComponent == null ? "" : parentComponent.FullName,
                Interfaces = drawable2dCode.GetInterfaces().Select((i) => { return i.FullName; }).ToList(),
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
