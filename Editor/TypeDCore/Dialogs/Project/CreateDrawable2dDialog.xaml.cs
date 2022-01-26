using System.IO;
using System.Linq;
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
            bool isValid = !string.IsNullOrEmpty(EntityName) &&
                            EntityName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1 &&
                            (char.IsLetter(EntityName.FirstOrDefault()) || EntityName.StartsWith("_"));
            if (!isValid)
            {
                MessageBox.Show($"Invalid name '{EntityName}'");
                return;
            }

            DialogResult = true;
            Close();
        }

        public string EntityName { get; set; }
        public string EntityNamespace { get; set; }
    }
}
