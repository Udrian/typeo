using Microsoft.Win32;
using System.Windows;
using TypeD;
using TypeD.Models;

namespace TypeDitor.Commands
{
    static class ProjectCommands
    {
        public static readonly CustomCommands OpenProject = new(async (sender) => {
            //Open project
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".typeo";
            openFileDialog.Filter = "TypeO projects (.typeo)|*.typeo";
            if (openFileDialog.ShowDialog() == true)
            {
                var loadedProject = await Command.Project.Load(openFileDialog.FileName);
                RecentModel.SaveRecents(loadedProject.ProjectFilePath, loadedProject.ProjectName);

                OpenMainWindow();
            }
        });

        public static readonly CustomCommands NewProject = new((sender) => {
            //New project
            OpenMainWindow();
        });

        private static void OpenMainWindow()
        {
            var currentMainWindow = Application.Current.MainWindow;
            var mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            currentMainWindow.Close();
        }

    }
}
