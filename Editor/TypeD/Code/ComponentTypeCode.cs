using System;
using TypeD.Models.Data;

namespace TypeD.Code
{
    public abstract class ComponentTypeCode : TypeDCodalyzer
    {
        // Properties
        public abstract Type TypeOBaseType { get; }
        public bool IsBaseComponentType
        {
            get
            {
                return BaseClass == TypeOBaseType.FullName;
            }
        }
        public Component ParentComponent { get; private set; }

        // Constructors
        public ComponentTypeCode(string className, string @namespace, Component parentComponentType)
        {
            ClassName = className;
            Namespace = @namespace;
            ParentComponent = parentComponentType;
            BaseClass = ParentComponent == null ? TypeOBaseType.FullName : ParentComponent.FullName;
        }
    }
}
