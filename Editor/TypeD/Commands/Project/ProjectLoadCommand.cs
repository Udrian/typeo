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

                    foreach(var typeDType in project.TypeOTypes.Values)
                    {
                        if (typeDType.TypeInfo == null) continue;

                        switch (typeDType.TypeOBaseType)
                        {
                            //TODO: Fix, this should be handled in modules
                            case "Game":
                                project.AddCode(new GameCode(project));
                                project.AddCode(new GameTypeDCode(project));
                                break;
                            case "Scene":
                                //TODO: Fix project.AddCode(new SceneCode(project, typeDType.Name, typeDType.Namespace));
                                //TODO: Fix project.AddCode(new SceneTypeDCode(project, typeDType.Name, typeDType.Namespace));
                                break;
                            case "Entity":
                                //TODO: Fix project.AddCode(new EntityCode(project, typeDType.Name, typeDType.Namespace));
                                //TODO: Fix project.AddCode(new EntityTypeDCode(project, typeDType.Name, typeDType.Namespace));
                                break;
                            case "Stub":
                                break;
                            case "Logic":
                                break;
                            case "Drawable":
                                break;
                            case "EntityData":
                                break;
                            default:
                                break;
                        }
                    }

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
