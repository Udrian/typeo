using System.Collections.Generic;
using System.Linq;
using TypeD.Models.Data;
using TypeD.Models.DTO;
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
            @namespace = ProjectModel.TransformNamespaceString(project, @namespace);

            var interfaces = new List<string>();
            if (updatable)
            {
                interfaces.Add(typeof(IUpdatable).FullName);
            }
            if (drawable)
            {
                interfaces.Add(typeof(IDrawable).FullName);
            }

            ComponentProvider.Create<EntityCode>(
                project,
                className,
                @namespace,
                parentComponent,
                interfaces
            );
        }

        public void CreateScene(Project project, string className, string @namespace, Component parentComponent)
        {
            @namespace = ProjectModel.TransformNamespaceString(project, @namespace);

            ComponentProvider.Create<SceneCode>(
                project,
                className,
                @namespace,
                parentComponent
            );
        }

        public void CreateDrawable2d(Project project, string className, string @namespace, Component parentComponent)
        {
            @namespace = ProjectModel.TransformNamespaceString(project, @namespace);

            ComponentProvider.Create<Drawable2dCode>(
                project,
                className,
                @namespace,
                parentComponent
            );
        }
    }
}
