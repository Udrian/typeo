using System.Collections.Generic;
using TypeD.Models.Data;

namespace TypeD.Models.Providers.Interfaces
{
    public interface IComponentProvider
    {
        public Component Load(Project project, string fullName);
        public void Save(Project project, Component component);
        public bool Exists(Project project, Component component);
        public List<Component> ListAll(Project project);
    }
}
