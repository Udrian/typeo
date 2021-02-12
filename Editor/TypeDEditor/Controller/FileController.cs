using System.Threading.Tasks;
using TypeD.Commands.Project;
using TypeDEditor.Model;

namespace TypeDEditor.Controller
{
    public static class FileController
    {
        public static async Task Create(string projectName, string location, string csSolutionPath, string csProjName)
        {
            if (ProjectController.LoadedProject != null)
                ProjectCommand.Clear(ProjectController.LoadedProject);
            ProjectController.LoadedProject = await ProjectCommand.Create(projectName, location, csSolutionPath, csProjName);

            RecentModel.SaveRecent(ProjectController.LoadedProject.ProjectFilePath, ProjectController.LoadedProject.ProjectName);
        }

        public static async Task Open(string projectFilePath)
        {
            if (ProjectController.LoadedProject != null)
                ProjectCommand.Clear(ProjectController.LoadedProject);
            ProjectController.LoadedProject = await ProjectCommand.Load(projectFilePath);

            RecentModel.SaveRecent(ProjectController.LoadedProject.ProjectFilePath, ProjectController.LoadedProject.ProjectName);
        }

        public static async Task Save()
        {
            if (ProjectController.LoadedProject == null) return;
                await ProjectCommand.Save(ProjectController.LoadedProject);
        }
    }
}
