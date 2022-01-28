using Ookii.Dialogs.Wpf;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace TypeDCore.Dialogs.Project
{
    /// <summary>
    /// Interaction logic for CreateDrawable2dDialog.xaml
    /// </summary>
    public partial class CreateDrawable2dDialog : Window, INotifyPropertyChanged
    {
        TypeD.Models.Data.Project Project { get; set; }

        public CreateDrawable2dDialog(TypeD.Models.Data.Project project)
        {
            InitializeComponent();
            Project = project;
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
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

            if (File.Exists(@$"{Project.ProjectTypeOPath}\components\{Project.ProjectName}\{EntityNamespace.Replace(".", "\\")}\{EntityName}.component"))
            {
                MessageBox.Show($"'{EntityNamespace}.{EntityName}' already exists");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void btnOpenNamespace_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.SelectedPath = @$"{Project.ProjectSourcePath}\{EntityNamespace.Replace(".", "\\")}";
            if (folderBrowserDialog.ShowDialog() == true)
            {
                EntityNamespace = folderBrowserDialog.SelectedPath.Replace("\\", ".").Substring(@$"{Project.ProjectSourcePath}\".Length);

                NotifyPropertyChanged("EntityNamespace");
            }
        }

        public string EntityName { get; set; }
        public string EntityNamespace { get; set; }
    }
}
