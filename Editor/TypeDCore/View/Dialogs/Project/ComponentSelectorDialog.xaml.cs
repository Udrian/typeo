using System.Windows;
using TypeD.Models.Data;
using TypeDCore.ViewModel.Dialogs.Project;

namespace TypeDCore.View.Dialogs.Project
{
    /// <summary>
    /// Interaction logic for ComponentSelectorDialog.xaml
    /// </summary>
    public partial class ComponentSelectorDialog : Window
    {
        // ViewModel
        public ComponentSelectorViewModel ViewModel { get; set; }

        // Constructors
        public ComponentSelectorDialog(TypeD.Models.Data.Project project)
        {
            InitializeComponent();
            ViewModel = new ComponentSelectorViewModel(this, project);
            this.DataContext = ViewModel;
        }

        // Event Handlers
        private void lbComponents_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (lbComponents.SelectedItem is not Component component) return;
            ViewModel.SelectedComponents = component;
            DialogResult = true;
            Close();
        }

        private void tbFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ViewModel.FilteredName = tbFilter.Text;
            ViewModel.UpdateFilter();
        }
    }
}
