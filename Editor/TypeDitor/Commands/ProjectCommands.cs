using Microsoft.Win32;
using System.IO;
using System.Windows;
using TypeD;
using TypeD.Models;
using TypeDitor.View.Project;

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
            var newProjectDialog = new NewProjectDialog();
            if(newProjectDialog.ShowDialog() == true)
            {
                var name = Path.GetFileNameWithoutExtension(newProjectDialog.ProjectName);
                var location = IsDirectory(newProjectDialog.ProjectLocation) ? newProjectDialog.ProjectLocation : Path.GetDirectoryName(newProjectDialog.ProjectLocation);
                var solution = @$".\{newProjectDialog.ProjectCSSolutionName}.sln";
                var project = Path.GetFileNameWithoutExtension(newProjectDialog.ProjectCSProjectName);

                //New project
                var progressDialog = new ProjectCreationProgressDialog();
                var newProjectTask = Command.Project.Create(name,
                                                              location,
                                                              solution,
                                                              project,
                                                              (progress) =>
                                                              {
                                                                  progressDialog.Progress = progress;
                                                                  if(progress >= 100)
                                                                  {
                                                                      progressDialog.Close();
                                                                  }
                                                              });
                progressDialog.ShowDialog();
                var newProject = newProjectTask.Result;
                RecentModel.SaveRecents(newProject.ProjectFilePath, newProject.ProjectName);
                OpenMainWindow(newProject);
            }
        });

        private static bool IsDirectory(string filePath)
        {
            return Path.GetFileName(filePath) == Path.GetFileNameWithoutExtension(filePath);
        }

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
