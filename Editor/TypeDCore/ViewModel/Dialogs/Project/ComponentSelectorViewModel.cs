using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        // Properties
        public List<Component> AllComponents { get; set; }
        public List<Component> FilteredComponents { get; set; }
        public Component SelectedComponent { get; set; }
        public string FilteredTypes { get; set; }
        public string ExcludedTypes { get; set; }
        public string IncludedTypes { get; set; }
        public string FilteredNames { get; set; }
        public string ExcludedNames { get; set; }
        public string IncludedNames { get; set; }
        public string FilterSeperator { get; set; } = ";";

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
            if (string.IsNullOrEmpty(FilteredTypes)) FilteredTypes = "";
            if (string.IsNullOrEmpty(ExcludedTypes)) ExcludedTypes = "";
            if (string.IsNullOrEmpty(IncludedTypes)) IncludedTypes = "";
            if (string.IsNullOrEmpty(FilteredNames)) FilteredNames = "";
            if (string.IsNullOrEmpty(ExcludedNames)) ExcludedNames = "";
            if (string.IsNullOrEmpty(IncludedNames)) IncludedNames = "";

            var filtered = new List<Component>();

            foreach (var component in AllComponents)
            {
                if (Filter(component.TypeOBaseType.FullName,
                    FilteredTypes.Split(FilterSeperator).Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)),
                    ExcludedTypes.Split(FilterSeperator).Select(s => s.Trim()),
                    IncludedTypes.Split(FilterSeperator).Select(s => s.Trim())))
                {
                    continue;
                }
                if (Filter(component.FullName,
                    FilteredNames.Split(FilterSeperator).Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)),
                    ExcludedNames.Split(FilterSeperator).Select(s => s.Trim()),
                    IncludedNames.Split(FilterSeperator).Select(s => s.Trim())))
                {
                    continue;
                }

                filtered.Add(component);
            }

            FilteredComponents = filtered;
            OnPropertyChanged(nameof(FilteredComponents));
        }

        private class FilterComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return y.ToLower().Contains(x.ToLower());
            }

            public int GetHashCode([DisallowNull] string obj)
            {
                return obj.GetHashCode();
            }
        }

        private bool Filter(string text, IEnumerable<string> filter, IEnumerable<string> exclude, IEnumerable<string> include)
        {
            var filterResult = false;
            if (filter.Any(f => f != "") && !filter.Contains(text, new FilterComparer())) filterResult = true;
            if (exclude.Contains(text)) filterResult = true;
            if (include.Contains(text)) filterResult = false;

            return filterResult;
        }
    }
}
