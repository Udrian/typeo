using Microsoft.Win32;
using System.Windows;
using TypeD;
using TypeD.Models;

namespace TypeDitor.Commands
{
    static class ProjectCommands
    {
        public static readonly CustomCommands OpenProject = new(async (sender) => {

            var path = "";
            if (sender is RecentModel)
            {
                path = (sender as RecentModel).Path;
            }
            else
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".typeo";
                openFileDialog.Filter = "TypeO projects (.typeo)|*.typeo";
                if (openFileDialog.ShowDialog() == true)
                {
                    path = openFileDialog.FileName;
                }
            }

            //Open project
            if (!string.IsNullOrEmpty(path))
            {
                var loadedProject = await Command.Project.Load(path);
                RecentModel.SaveRecents(loadedProject.ProjectFilePath, loadedProject.ProjectName);

                OpenMainWindow(loadedProject);
            }
        });

        public static readonly CustomCommands NewProject = new((sender) => {
            //New project
            OpenMainWindow(null);
        });

        private static void OpenMainWindow(ProjectModel loadedProject)
        {
            var currentMainWindow = Application.Current.MainWindow;
            var mainWindow = new MainWindow(loadedProject);
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            currentMainWindow.Close();
        }

    }
}
