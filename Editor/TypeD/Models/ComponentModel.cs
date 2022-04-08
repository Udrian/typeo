using System;
using System.Linq;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

namespace TypeD.Models
{
    public class ComponentModel : IComponentModel
    {
        // Provider
        IComponentProvider ComponentProvider { get; set; }

        // Model
        IUINotifyModel UINotifyModel { get; set; }
        IProjectModel ProjectModel { get; set; }
        IHookModel HookModel { get; set; }

        // Constructors
        public ComponentModel() { }

        public void Init(IResourceModel resourceModel)
        {
            ComponentProvider = resourceModel.Get<IComponentProvider>();
            UINotifyModel = resourceModel.Get<IUINotifyModel>();
            ProjectModel = resourceModel.Get<IProjectModel>();
            HookModel = resourceModel.Get<IHookModel>();
        }

        public void Add(Project project, Component parent, Component child)
        {
            parent.Children.Add(child);
            ComponentProvider.Save(project, parent);
            ProjectModel.SaveCode(parent.Template.Code);

            HookModel.Shoot(new ComponentFocusHook() { Project = project, Component = parent });
        }

        public Type GetType(Component component)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(component.FullName));
        }
    }
}
