using System.Reflection;
using TypeD.Models;
using TypeD.Types;

namespace TypeDCore.Code.Entity
{
    class EntityTypeOType : TypeOType
    {
        public EntityTypeOType(string className, string @namespace, string typeOBaseType, TypeInfo typeInfo, ProjectModel project) : base(className, @namespace, typeOBaseType, typeInfo, project)
        {
            Codes.Add(new EntityCode(Project, ClassName, Namespace));
            Codes.Add(new EntityTypeDCode(Project, ClassName, Namespace));
        }
    }
}
