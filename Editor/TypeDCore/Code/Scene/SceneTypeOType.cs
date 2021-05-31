using System.Reflection;
using TypeD.Models;
using TypeD.Types;

namespace TypeDCore.Code.Scene
{
    class SceneTypeOType : TypeOType
    {
        public SceneTypeOType(string className, string @namespace, string typeOBaseType, TypeInfo typeInfo, ProjectModel project) : base(className, @namespace, typeOBaseType, typeInfo, project)
        {
            Codes.Add(new SceneCode(Project, ClassName, Namespace));
            Codes.Add(new SceneTypeDCode(Project, ClassName, Namespace));
        }
    }
}
