using Ookii.Dialogs.Wpf;
using System.IO;
using System.Linq;
using System.Windows;
using TypeD.ViewModel;
using TypeDCore.View.Dialogs.Project;

namespace TypeDCore.ViewModel.Dialogs.Project
{
    public class CreateComponentTypeBaseViewModel : ViewModelBase
    {
        // Data
        TypeD.Models.Data.Project Project { get; set; }

        // Constructors
        public CreateComponentTypeBaseViewModel(TypeD.Models.Data.Project project, string @namespace, string inherits)
        {
            Project = project;

            ComponentNamespace = @namespace;
            ComponentInherits = inherits;
        }

        // Functions
        public virtual bool Validate()
        {
            bool isValid = !string.IsNullOrEmpty(ComponentName) &&
                            ComponentName.IndexOfAny(Path.GetInvalidFileNameChars()) == -1 &&
                            !ComponentName.Contains(" ") &&
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
                OnPropertyChanged(nameof(ComponentNamespace));
            }
        }

        public void OpenInherit()
        {
            var dialog = new ComponentSelectorDialog(Project);
            dialog.ViewModel.FilteredType = ComponentBaseType;
            dialog.ViewModel.UpdateFilter();

            if (dialog.ShowDialog() == true && dialog.ViewModel.SelectedComponents != null)
            {
                ComponentInherits = dialog.ViewModel.SelectedComponents.FullName;
                OnPropertyChanged(nameof(ComponentInherits));
            }
        }

        // Properties
        public string ComponentName { get; set; }
        public string ComponentNamespace { get; set; }
        public string ComponentInherits { get; set; }

        public string ComponentBaseType { get; set; }
    }
}
