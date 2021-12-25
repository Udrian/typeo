using System;
using TypeD.Models.Data;

namespace TypeD.Models.Interfaces
{
    public interface IComponentModel
    {
        public Type GetType(Component component);
    }
}
