using System;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class ComponentModel : IComponentModel
    {
        public Type GetType(Component component)
        {
            return Type.GetType(component.FullName);
        }
    }
}
