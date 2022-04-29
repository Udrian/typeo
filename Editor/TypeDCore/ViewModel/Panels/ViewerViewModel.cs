using System.Windows.Controls;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.ViewModel;
using TypeDCore.View.Panels;
using TypeDCore.View.Viewer;

namespace TypeDCore.ViewModel.Panels
{
    internal class ViewerViewModel : ViewModelBase
    {
        // Models
        IHookModel HookModel { get; set; }

        // Data
        ViewerPanel ViewerPanel { get; set; }
        Project Project { get; set; }

        // Constructors
        public ViewerViewModel(Project project, ViewerPanel viewerPanel) : base(viewerPanel)
        {
            ViewerPanel = viewerPanel;
            Project = project;
            HookModel = ResourceModel.Get<IHookModel>();

            HookModel.AddHook<OpenComponentHook>(ComponentOpened);
            HookModel.AddHook<CloseComponentHook>(ComponentClosed);
            HookModel.AddHook<ComponentFocusHook>(ComponentFocus);
        }

        // Functions
        public void Unload()
        {
            HookModel.RemoveHook<OpenComponentHook>(ComponentOpened);
            HookModel.RemoveHook<CloseComponentHook>(ComponentClosed);
            HookModel.RemoveHook<ComponentFocusHook>(ComponentFocus);
        }

        public void TabSelectionChanged(ConsoleViewer consoleViewer)
        {
            if (consoleViewer == null)
                return;
            HookModel.Shoot(new ComponentFocusHook() { Project = Project, Component = consoleViewer.Component});
        }

        void ComponentOpened(OpenComponentHook hook)
        {
            ViewerPanel.Tabs.Items.Add(new TabItem() {
                Header = $"{hook.Component.ClassName}",
                Content = new ConsoleViewer(hook.Project, hook.Component)
            });
        }

        void ComponentClosed(CloseComponentHook hook)
        {
            TabItem foundItem = null;
            foreach(var item in ViewerPanel.Tabs.Items)
            {
                if(((item as TabItem).Content as ConsoleViewer).Component.FullName == hook.Component.FullName)
                {
                    foundItem = item as TabItem;
                    break;
                }    
            }
            ViewerPanel.Tabs.Items.Remove(foundItem);
        }

        void ComponentFocus(ComponentFocusHook hook)
        {
            TabItem foundItem = null;
            foreach (var item in ViewerPanel.Tabs.Items)
            {
                if (((item as TabItem).Content as ConsoleViewer).Component.FullName == hook.Component.FullName)
                {
                    foundItem = item as TabItem;
                    break;
                }
            }
            if (foundItem != null)
                foundItem.IsSelected = true;
        }
    }
}
