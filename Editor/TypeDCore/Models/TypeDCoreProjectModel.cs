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
            ProjectModel.AddCode(project, entityCode);

            var interfaces = entityCode.GetInterfaces();
            if(updatable && !interfaces.Contains(typeof(IUpdatable)))
            {
                interfaces.Add(typeof(IUpdatable));
            }
            if (drawable && !interfaces.Contains(typeof(IDrawable)))
            {
                interfaces.Add(typeof(IDrawable));
            }

            ComponentProvider.Save(project, new Component()
            {
                ClassName = entityCode.ClassName,
                Namespace = entityCode.Namespace,
                ParentComponent = parentComponent,
                Interfaces = interfaces,
                TemplateClass = new List<string>()
                {
                    typeof(EntityCode).FullName
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

            ComponentProvider.Save(project, new Component()
            {
                ClassName = sceneCode.ClassName,
                Namespace = sceneCode.Namespace,
                ParentComponent = parentComponent,
                Interfaces = sceneCode.GetInterfaces(),
                TemplateClass = new List<string>()
                {
                    typeof(SceneCode).FullName
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
                ParentComponent = parentComponent,
                Interfaces = drawable2dCode.GetInterfaces(),
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
