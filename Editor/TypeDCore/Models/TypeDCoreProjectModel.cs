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
            var code = new EntityCode(className, @namespace, parentComponent, updatable, drawable);
            ProjectModel.InitAndSaveCode(project, code);

            var component = new Component(code, parentComponent);
            if(updatable && !component.Interfaces.Contains(typeof(IUpdatable)))
            {
                component.Interfaces.Add(typeof(IUpdatable));
            }
            if (drawable && !component.Interfaces.Contains(typeof(IDrawable)))
            {
                component.Interfaces.Add(typeof(IDrawable));
            }

            ComponentProvider.Save(project, component);

            ProjectModel.BuildComponentTree(project);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }

        public void CreateScene(Project project, string className, string @namespace, Component parentComponent)
        {
            @namespace = (@namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}").Replace("\\", ".").Replace("/", ".");
            var code = new SceneCode(className, @namespace, parentComponent);
            ProjectModel.InitAndSaveCode(project, code);

            ComponentProvider.Save(project, new Component(code, parentComponent));
            ProjectModel.BuildComponentTree(project);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }

        public void CreateDrawable2d(Project project, string className, string @namespace, Component parentComponent)
        {
            @namespace = (@namespace.StartsWith(project.ProjectName) ? @namespace : $"{project.ProjectName}.{@namespace}").Replace("\\", ".").Replace("/", ".");
            var code = new Drawable2dCode(className, @namespace, parentComponent);
            ProjectModel.InitAndSaveCode(project, code);

            ComponentProvider.Save(project, new Component(code, parentComponent));
            ProjectModel.BuildComponentTree(project);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }
    }
}
