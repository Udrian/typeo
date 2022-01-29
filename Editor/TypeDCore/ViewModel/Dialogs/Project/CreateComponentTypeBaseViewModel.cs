using Ookii.Dialogs.Wpf;
using System.IO;
using System.Linq;
using System.Windows;
using TypeD.ViewModel;

namespace TypeDCore.ViewModel.Dialogs.Project
{
    public class CreateComponentTypeBaseViewModel : ViewModelBase
    {
        // Data
        TypeD.Models.Data.Project Project { get; set; }

        // Constructors
        public CreateComponentTypeBaseViewModel(TypeD.Models.Data.Project project)
        {
            Project = project;
        }

        // Functions
        public virtual bool Validate()
        {
            bool isValid = !string.IsNullOrEmpty(ComponentName) &&
                            ComponentName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1 &&
                            (char.IsLetter(ComponentName.FirstOrDefault()) || ComponentName.StartsWith("_"));
            if (!isValid)
            {
                MessageBox.Show($"Invalid name '{ComponentName}'");
                return false;
            }

            if (File.Exists(@$"{Project.ProjectTypeOPath}\components\{Project.ProjectName}\{ComponentNamespace.Replace(".", "\\")}\{ComponentName}.component"))
            {
                MessageBox.Show($"'{ComponentNamespace}.{ComponentName}' already exists");
                return false;
            }

            return true;
        }

        public void OpenNamespace()
        {
            var folderBrowserDialog = new VistaFolderBrowserDialog();
            folderBrowserDialog.SelectedPath = @$"{Project.ProjectSourcePath}\{ComponentNamespace.Replace(".", "\\")}";
            if (folderBrowserDialog.ShowDialog() == true)
            {
                ComponentNamespace = folderBrowserDialog.SelectedPath.Replace("\\", ".").Substring(@$"{Project.ProjectSourcePath}\".Length);

                OnPropertyChanged(ComponentNamespace);
            }
        }

        // Properties
        public string ComponentName { get; set; }
        public string ComponentNamespace { get; set; }
    }
}
