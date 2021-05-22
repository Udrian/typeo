using TypeD.Types;

namespace TypeD.TreeNodes
{
    public class ItemCode : Item
    {
        public TypeOType TypeOType { get; set; }

        public ItemCode(TypeOType typeOType)
        {
            TypeOType = typeOType;
        }

        public override void Save()
        {
            TypeOType.Save();
        }
    }
}
