using System.Threading.Tasks;
using TypeD;
using TypeDEditor.Model;

namespace TypeDEditor.Controller
{
    public static class FileController
    {
        public static async Task Create(string projectName, string location, string csSolutionPath, string csProjName)
        {
            if (ProjectController.LoadedProject != null)
                Command.Project.Clear(ProjectController.LoadedProject);
            ProjectController.LoadedProject = await Command.Project.Create(projectName, location, csSolutionPath, csProjName);

            RecentModel.SaveRecent(ProjectController.LoadedProject.ProjectFilePath, ProjectController.LoadedProject.ProjectName);
        }

        public static async Task Open(string projectFilePath)
        {
            if (ProjectController.LoadedProject != null)
                Command.Project.Clear(ProjectController.LoadedProject);
            ProjectController.LoadedProject = await Command.Project.Load(projectFilePath);

            RecentModel.SaveRecent(ProjectController.LoadedProject.ProjectFilePath, ProjectController.LoadedProject.ProjectName);
        }

        public static async Task Save()
        {
            if (ProjectController.LoadedProject == null) return;
                await Command.Project.Save(ProjectController.LoadedProject);
        }
    }
}
