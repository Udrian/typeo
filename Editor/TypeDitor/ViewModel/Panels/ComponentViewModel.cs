using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.ViewModel;

namespace TypeDitor.ViewModel.Panels
{
    public class ComponentViewModel : ViewModelBase
    {
        // Definitions
        public class Node
        {
            public Component Component { get; set; }

            public string Title { get => Component.ClassName; }
            public ObservableCollection<Node> Nodes { get => new ObservableCollection<Node>(Component.Children.Select(c => new Node(c))); }

            public Node(Component component)
            {
                Component = component;
            }
        }

        // Models
        public IHookModel HookModel { get; set; }

        // Properties
        private Component _component;
        public Component Component
        {
            get => _component;
            set
            {
                _component = value;
                Nodes.Clear();
                Nodes.Add(new Node(_component));
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Node> Nodes { get; set; }

        // Constructors
        public ComponentViewModel(FrameworkElement element, Project project) : base(element)
        {
            HookModel = ResourceModel.Get<IHookModel>();
            Nodes = new ObservableCollection<Node>();

            HookModel.AddHook<ComponentFocusHook>((hook) =>
            {
                Component = hook.Component;
            });
        }
    }
}
