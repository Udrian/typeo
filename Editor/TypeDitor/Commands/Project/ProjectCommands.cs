using System.IO;
using System.Windows;
using TypeDitor.View;

namespace TypeDitor.Commands.Project
{
    class ProjectCommands : CustomCommands
    {
        protected bool IsDirectory(string filePath)
        {
            return Path.GetFileName(filePath) == Path.GetFileNameWithoutExtension(filePath);
        }

        protected void OpenMainWindow(TypeD.Models.Data.Project loadedProject)
        {
            var currentMainWindow = Application.Current.MainWindow;
            var mainWindow = new MainWindow(loadedProject);
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            currentMainWindow.Close();
        }
    }
}
