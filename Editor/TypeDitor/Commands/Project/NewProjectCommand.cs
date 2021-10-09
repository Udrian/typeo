using System.IO;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.Dialog.Project;

namespace TypeDitor.Commands.Project
{
    class NewProjectCommand : ProjectCommands
    {
        // Providers
        private IRecentProvider RecentProvider { get; set; }
        private IProjectProvider ProjectProvider { get; set; }

        public NewProjectCommand(IRecentProvider recentProvider, IProjectProvider projectProvider)
        {
            RecentProvider = recentProvider;
            ProjectProvider = projectProvider;
        }

        public override void Execute(object param)
        {
            var newProjectDialog = new NewProjectDialog();
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
