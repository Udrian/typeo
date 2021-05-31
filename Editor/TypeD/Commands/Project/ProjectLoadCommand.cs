using System.IO;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Data;
using TypeD.Helpers;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public partial class ProjectCommand
    {
        public async Task<ProjectModel> Load(string projectFilePath)
        {
            if (!projectFilePath.EndsWith(".typeo")) return null;

            try
            {
                var task = new Task<ProjectModel>(() =>
                {
                    var projectData = JSON.Deserialize<ProjectData>(projectFilePath);
                    var project = new ProjectModel(Path.GetDirectoryName(projectFilePath), projectData);

                    // Prepare
                    project.AddCode(new ProgramCode(project));

                    project.BuildTree();

                    return project;
                });

                task.Start();
                return await task;
            }
            catch
            {
                return null;
            }
        }
    }
}
