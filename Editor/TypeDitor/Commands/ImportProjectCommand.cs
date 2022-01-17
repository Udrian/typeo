using Microsoft.Win32;
using System.IO;
using TypeD.Models.Data;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.View.Dialogs.Project;

namespace TypeDitor.Commands
{
    class ImportProjectCommand : ProjectCommands
    {
        // Providers
        private IRecentProvider RecentProvider { get; set; }
        private IProjectProvider ProjectProvider { get; set; }

        public ImportProjectCommand(IRecentProvider recentProvider, IProjectProvider projectProvider)
        {
            RecentProvider = recentProvider;
            ProjectProvider = projectProvider;
        }

        public override void Execute(object param)
        {
            var path = "";
            if (param is Recent)
            {
                path = (param as Recent).Path;
            }
            else
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".sln";
                openFileDialog.Filter = "Solution Files (*.sln)|*.sln";
                if (openFileDialog.ShowDialog() == true)
                {
                    path = openFileDialog.FileName;
                }
            }

            //Open project
            if (!string.IsNullOrEmpty(path))
            {
                var newProjectDialog = new NewProjectDialog();
                newProjectDialog.ProjectName = Path.GetFileNameWithoutExtension(path);

                var projectLocation = Path.GetDirectoryName(path);
                projectLocation = projectLocation.Substring(0, projectLocation.LastIndexOf(newProjectDialog.ProjectName)).TrimEnd('\\').TrimEnd('/');
                newProjectDialog.ProjectLocation = projectLocation;

                if (newProjectDialog.ShowDialog() == true)
                {
                    var name = Path.GetFileNameWithoutExtension(newProjectDialog.ProjectName);
                    var location = this.IsDirectory(newProjectDialog.ProjectLocation) ? newProjectDialog.ProjectLocation : Path.GetDirectoryName(newProjectDialog.ProjectLocation);
                    var solution = @$".\{newProjectDialog.ProjectCSSolutionName}.sln";
                    var project = Path.GetFileNameWithoutExtension(newProjectDialog.ProjectCSProjectName);

                    //New project
                    var progressDialog = new ProjectCreationProgressDialog();
                    var newProjectTask = ProjectProvider.Create(name,
                                                                  location,
                                                                  solution,
                                                                  project,
                                                                  (progress) =>
                                                                  {
                                                                      progressDialog.Progress = progress;
                                                                      if (progress >= 100)
                                                                      {
                                                                          progressDialog.Close();
                                                                      }
                                                                  });
                    progressDialog.ShowDialog();
                    var newProject = newProjectTask.Result;
                    RecentProvider.Add(newProject.ProjectFilePath, newProject.ProjectName);
                    this.OpenMainWindow(newProject);
                }
            }
        }
    }
}
