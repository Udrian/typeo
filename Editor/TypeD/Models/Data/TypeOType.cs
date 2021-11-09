using System;
using System.Collections.Generic;

namespace TypeD.Models.Data
{
    public class TypeOType
    {
        public string ClassName { get; set; }
        public string Namespace { get; set; }
        public List<string> TemplateClass { get; set; }
        public string FullName { get { return $"{Namespace}.{ClassName}"; } }
        public string TypeOBaseType { get; set; }
    }
}
