using System.Windows;
using TypeDCore.ViewModel.Dialogs.Project;

namespace TypeDCore.View.Dialogs.Project
{
    /// <summary>
    /// Interaction logic for CreateEntityTypeDialog.xaml
    /// </summary>
    public partial class CreateEntityTypeDialog : Window
    {
        // ViewModel
        public CreateEntityTypeViewModel ViewModel { get; set; }

        // Constructors
        public CreateEntityTypeDialog(TypeD.Models.Data.Project project)
        {
            InitializeComponent();
            ViewModel = new CreateEntityTypeViewModel(project);
            this.DataContext = ViewModel;
        }

        // Events
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
    }
}
