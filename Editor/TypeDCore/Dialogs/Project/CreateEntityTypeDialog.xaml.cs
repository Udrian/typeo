using System.Windows;

namespace TypeDCore.Dialogs.Project
{
    /// <summary>
    /// Interaction logic for CreateEntityTypeDialog.xaml
    /// </summary>
    public partial class CreateEntityTypeDialog : Window
    {
        public CreateEntityTypeDialog()
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
        public bool EntityUpdatable { get; set; }
        public bool EntityDrawable { get; set; }


    }
}
