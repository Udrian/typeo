using System.Windows;

namespace TypeDCore.Dialogs.Project
{
    /// <summary>
    /// Interaction logic for CreateDrawable2dDialog.xaml
    /// </summary>
    public partial class CreateDrawable2dDialog : Window
    {
        public CreateDrawable2dDialog()
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
