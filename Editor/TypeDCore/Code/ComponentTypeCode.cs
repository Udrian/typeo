using System;
using TypeD;

namespace TypeDCore.Code
{
    public abstract class ComponentTypeCode : Codalyzer
    {
        // Properties
        public abstract Type BaseComponentType { get; }
        public bool IsBaseComponentType
        {
            get
            {
                return BaseClass == BaseComponentType.FullName;
            }
        }

        // Constructors
        public ComponentTypeCode(string className, string @namespace, string baseClass)
        {
            Init(className, @namespace);

            BaseClass = string.IsNullOrEmpty(baseClass) ? BaseComponentType.FullName : baseClass;
        }
    }
}
