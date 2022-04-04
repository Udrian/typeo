using System;
using System.Linq;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class ComponentModel : IComponentModel
    {
        // Constructors
        public ComponentModel() { }

        public void Init(IResourceModel resourceModel)
        {
        }

        public Type GetType(Component component)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(component.FullName));
        }
    }
}
