using System.Collections.Generic;
using TypeD;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDCore.Code.Drawable2d;
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
        public void CreateEntity(Project project, string className, string @namespace, bool updatable, bool drawable)
        {
            @namespace = @namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}";
            var entityCode = new EntityCode(className, @namespace);

            if (updatable)
            {
                entityCode.Usings.Add("TypeOEngine.Typedeaf.Core.Interfaces");
                entityCode.AddInterface("IUpdatable");
                entityCode.AddProperty(new Codalyzer.Property("public bool Pause"));
                entityCode.AddFunction(new Codalyzer.Function("public void Update(double dt)", () => { }));
            }
            if (drawable)
            {
                entityCode.Usings.Add("TypeOEngine.Typedeaf.Core.Entities.Interfaces");
                entityCode.Usings.Add("TypeOEngine.Typedeaf.Core.Engine.Graphics");
                entityCode.AddInterface("IDrawable");
                entityCode.AddProperty(new Codalyzer.Property("public bool Hidden"));
                entityCode.AddProperty(new Codalyzer.Property("public int DrawOrder"));
                entityCode.AddFunction(new Codalyzer.Function("public void Draw(Canvas canvas)", () => { }));
            }

            ProjectModel.AddCode(project, entityCode);
            ProjectModel.AddCode(project, new EntityTypeDCode(className, @namespace));
            ComponentProvider.Save(project, new Component()
            {
                ClassName = entityCode.ClassName,
                Namespace = entityCode.Namespace,
                TemplateClass = new List<string>()
                {
                    typeof(EntityCode).FullName,
                    typeof(EntityTypeDCode).FullName
                },
                TypeOBaseType = "Entity2D"
            });

            ProjectModel.BuildComponentTree(project);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }

        public void CreateScene(Project project, string className, string @namespace)
        {
            @namespace = @namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}";
            var sceneCode = new SceneCode(className, @namespace);
            ProjectModel.AddCode(project, sceneCode);
            ProjectModel.AddCode(project, new SceneTypeDCode(className, @namespace));
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

        public void CreateDrawable2d(Project project, string className, string @namespace)
        {
            @namespace = @namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}";
            var drawable2dCode = new Drawable2dCode(className, @namespace);
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
