using System;
using TypeD.Models.Data;

namespace TypeD.Models.Interfaces
{
    public interface IComponentModel : IModel
    {
        public void Add(Project project, Component parent, Component child);
        public Type GetType(Component component);
    }
}
