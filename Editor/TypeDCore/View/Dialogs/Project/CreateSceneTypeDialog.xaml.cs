using System.Windows;
using TypeDCore.ViewModel.Dialogs.Project;

namespace TypeDCore.View.Dialogs.Project
{
    /// <summary>
    /// Interaction logic for CreateSceneDialog.xaml
    /// </summary>
    public partial class CreateSceneTypeDialog : Window
    {
        // ViewModel
        public CreateComponentTypeBaseViewModel ViewModel { get; set; }

        // Constructors
        public CreateSceneTypeDialog(TypeD.Models.Data.Project project, string @namespace, string inherits)
        {
            InitializeComponent();
            ViewModel = new CreateComponentTypeBaseViewModel(project, @namespace, inherits);
            ViewModel.ComponentBaseType = "Scene";
            this.DataContext = ViewModel;
        }

        // Event Handlers
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.Validate())
                return;

            DialogResult = true;
            Close();
        }

        private void btnOpenNamespace_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenNamespace();
        }

        private void btnOpenInherit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenInherit();
        }
    }
}
