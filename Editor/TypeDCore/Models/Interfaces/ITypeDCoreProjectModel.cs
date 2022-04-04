using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeDCore.Models.Interfaces
{
    interface ITypeDCoreProjectModel : IModel
    {
        public void CreateEntity(Project project, string className, string @namespace, Component parentComponent, bool updatable, bool drawable);
        public void CreateScene(Project project, string className, string @namespace, Component parentComponent);
        public void CreateDrawable2d(Project project, string className, string @namespace, Component parentComponent);
    }
}
