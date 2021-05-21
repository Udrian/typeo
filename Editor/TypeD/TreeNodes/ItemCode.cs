using System.Collections.Generic;
using System.Reflection;

namespace TypeD.TreeNodes
{
    public abstract class ItemCode : Item
    {
        public string Namespace { get; set; }
        public string FullName { get { return $"{Namespace}.{Name}"; } }

        public List<Codalyzer> Codes { get; private set; }
        public TypeInfo TypeInfo { get; set; }

        public ItemCode()
        {
            Codes = new List<Codalyzer>();
        }

        public override void Save()
        {
            foreach(var code in Codes)
            {
                code.Generate();
                code.Save();
            }
        }
    }
}
