using System.Reflection;
using TypeD.Models;
using TypeD.Types;

namespace TypeDCore.Code.Drawable2d
{
    class Drawable2dTypeOType : TypeOType
    {
        public Drawable2dTypeOType(string className, string @namespace, string typeOBaseType, TypeInfo typeInfo, ProjectModel project) : base(className, @namespace, typeOBaseType, typeInfo, project)
        {
            Codes.Add(new Drawable2dCode(Project, ClassName, Namespace));
        }
    }
}
