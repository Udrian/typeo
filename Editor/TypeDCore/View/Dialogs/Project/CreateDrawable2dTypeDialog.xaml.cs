using System.Windows;
using TypeDCore.ViewModel.Dialogs.Project;

namespace TypeDCore.View.Dialogs.Project
{
    /// <summary>
    /// Interaction logic for CreateDrawable2dDialog.xaml
    /// </summary>
    public partial class CreateDrawable2dTypeDialog : Window
    {
        // ViewModel
        public CreateComponentTypeBaseViewModel ViewModel { get; set; }

        // Constructors
        public CreateDrawable2dTypeDialog(TypeD.Models.Data.Project project, string @namespace)
        {
            InitializeComponent();
            ViewModel = new CreateComponentTypeBaseViewModel(project, @namespace, typeof(TypeOEngine.Typedeaf.Core.Entities.Drawables.Drawable2d).FullName);
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
            ViewModel.OpenComponents();
        }
    }
}
