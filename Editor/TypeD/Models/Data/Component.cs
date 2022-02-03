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
        public Type TemplateClass { get; set; }
        public string FullName { get { return $"{Namespace}.{ClassName}"; } }
        public Component ParentComponent { get; set; }
        public List<Type> Interfaces { get; set; }
        public Type TypeOBaseType { get; set; }

        public Component()
        {
            Interfaces = new List<Type>();
        }

        public Component(ComponentTypeCode code, Component parent)
        {
            ClassName = code.ClassName;
            Namespace = code.Namespace;
            ParentComponent = parent;
            Interfaces = code.GetInterfaces();
            TemplateClass = code.GetType();
            TypeOBaseType = code.TypeOBaseType;
        }
    }
}
