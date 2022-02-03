using System;
using System.Collections.Generic;
using TypeD.Code;

namespace TypeD.Models.Data
{
    public class Component
    {
        // Properties
        public string ClassName { get; set; }
        public string Namespace { get; set; }
        public List<string> TemplateClass { get; set; }
        public string FullName { get { return $"{Namespace}.{ClassName}"; } }
        public Component ParentComponent { get; set; }
        public List<Type> Interfaces { get; set; }
        public string TypeOBaseType { get; set; }

        public Component()
        {
            TemplateClass = new List<string>();
            Interfaces = new List<Type>();
        }
    }
}
