using System.IO;
using TypeD;
using TypeD.Models.Interfaces;
using TypeDitor.View.Project;

namespace TypeDitor.Commands.Project
{
    class NewProjectCommand : ProjectCommands
    {
        private IRecentModel RecentModel { get; set; }

        public NewProjectCommand(IRecentModel recentModel)
        {
            RecentModel = recentModel;
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
                var newProjectTask = Command.Project.Create(name,
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
                RecentModel.Add(newProject.ProjectFilePath, newProject.ProjectName);
                this.OpenMainWindow(newProject);
            }
        }
    }
}
