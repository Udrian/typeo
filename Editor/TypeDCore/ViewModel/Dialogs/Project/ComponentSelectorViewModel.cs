using System.Collections.Generic;
using System.Windows;
using TypeD.Models.Data;
using TypeD.Models.Providers.Interfaces;
using TypeD.ViewModel;

namespace TypeDCore.ViewModel.Dialogs.Project
{
    public class ComponentSelectorViewModel : ViewModelBase
    {
        // Data
        TypeD.Models.Data.Project Project { get; set; }

        // Providers
        public IComponentProvider ComponentProvider { get; set; }

        // Constructors
        public ComponentSelectorViewModel(FrameworkElement element, TypeD.Models.Data.Project project) : base(element)
        {
            Project = project;

            ComponentProvider = ResourceModel.Get<IComponentProvider>();

            FilteredComponents = AllComponents = ComponentProvider.ListAll(Project);
        }

        // Functions
        public void UpdateFilter()
        {
            var filtered = new List<Component>();

            foreach(var component in AllComponents)
            {
                if (!string.IsNullOrEmpty(FilteredType) && component.TypeOBaseType.Name != FilteredType)
                    continue;
                if (!string.IsNullOrEmpty(FilteredName) && !component.ClassName.Contains(FilteredName))
                    continue;

                filtered.Add(component);
            }

            FilteredComponents = filtered;
            OnPropertyChanged(nameof(FilteredComponents));
        }

        // Properties
        public List<Component> AllComponents { get; set; }
        public List<Component> FilteredComponents { get; set; }
        public Component SelectedComponent { get; set; }
        public string FilteredType { get; set; }
        public string FilteredName { get; set; }
    }
}
