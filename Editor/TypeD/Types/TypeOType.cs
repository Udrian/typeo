using System;
using System.Collections.Generic;
using System.Reflection;

namespace TypeD.Types
{
    public class TypeOType
    {
        public static List<Type> TypeOTypeTypes { get; set; } = new();

        public static string GetBaseTypeOClassName(Type subType)
        {
            foreach(var type in TypeOTypeTypes)
            {
                if (subType.IsSubclassOf(type)) return type.Name;
            }

            return "";
        }

        public string ClassName { get; set; }
        public string Namespace { get; set; }
        public string FullName { get { return $"{Namespace}.{ClassName}"; } }
        public string TypeOBaseType { get; set; }

        public List<Codalyzer> Codes { get; private set; }
        public TypeInfo TypeInfo { get; set; }

        public TypeOType()
        {
            Codes = new List<Codalyzer>();
        }

        public void Save()
        {
            foreach (var code in Codes)
            {
                code.Generate();
                code.Save();
            }
        }
    }
}
