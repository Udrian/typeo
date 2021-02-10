using System.Threading.Tasks;
using TypeD.Commands.Project;
using TypeD.Models;
using TypeDEditor.Model;

namespace TypeDEditor.Controller
{
    public static class FileController
    {
        public static ProjectModel LoadedProject { get; set; }

        public static async Task Create(string projectName, string location, string csSolutionPath, string csProjName)
        {
            if (LoadedProject != null)
                ProjectCommand.Clear(LoadedProject);
            LoadedProject = await ProjectCommand.Create(projectName, location, csSolutionPath, csProjName);

            RecentModel.SaveRecent(LoadedProject.ProjectFilePath, LoadedProject.ProjectName);
        }

        public static async Task Open(string projectFilePath)
        {
            if (LoadedProject != null)
                ProjectCommand.Clear(LoadedProject);
            LoadedProject = await ProjectCommand.Load(projectFilePath);

            RecentModel.SaveRecent(LoadedProject.ProjectFilePath, LoadedProject.ProjectName);
        }

        public static async Task Save()
        {
            if (LoadedProject == null) return;
                await ProjectCommand.Save(LoadedProject);
        }
    }
}
