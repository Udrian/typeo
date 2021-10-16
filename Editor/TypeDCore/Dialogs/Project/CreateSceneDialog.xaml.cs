using System.Windows;

namespace TypeDCore.Dialogs.Project
{
    /// <summary>
    /// Interaction logic for CreateSceneDialog.xaml
    /// </summary>
    public partial class CreateSceneDialog : Window
    {
        public CreateSceneDialog()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public string EntityName { get; set; }
        public string EntityNamespace { get; set; }
    }
}
