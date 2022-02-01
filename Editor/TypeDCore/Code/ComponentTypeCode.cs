using System;
using TypeD.Code;
using TypeD.Models.Data;

namespace TypeDCore.Code
{
    public abstract class ComponentTypeCode : Codalyzer
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
            Init(className, @namespace);

            ParentComponent = parentComponentType;
            BaseClass = ParentComponent == null ? TypeOBaseType.FullName : ParentComponent.FullName;
        }
    }
}
