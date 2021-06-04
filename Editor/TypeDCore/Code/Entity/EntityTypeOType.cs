using TypeD.Types;

namespace TypeDCore.Code.Entity
{
    class EntityTypeOType : TypeOType
    {
        public override void Init()
        {
            Codes.Add(new EntityCode(Project, ClassName, Namespace));
            Codes.Add(new EntityTypeDCode(Project, ClassName, Namespace));
        }
    }
}
