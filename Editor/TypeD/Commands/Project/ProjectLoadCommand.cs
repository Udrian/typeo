using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Data;
using TypeD.Helpers;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static async Task<ProjectModel> Load(string projectFilePath)
        {
            if (!projectFilePath.EndsWith(".typeo")) return null;

            try
            {
                var projectData = JSON.Deserialize<ProjectData>(projectFilePath);
                var project = new ProjectModel(Path.GetDirectoryName(projectFilePath), projectData);

                // Prepare
                project.AddCode(new ProgramCode(project));
                project.AddCode(new GameCode(project));

                return project;
            }
            catch
            {
                return null;
            }
        }
    }
}
