using TypeD.Models.Data;

namespace TypeDCore.Models.Interfaces
{
    interface ITypeDCoreProjectModel
    {
        public void CreateEntity(Project project, string className, string @namespace, bool updatable, bool drawable);
    }
}
