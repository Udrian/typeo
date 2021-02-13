using System.IO;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Data;
using TypeD.Helpers;
using TypeD.Models;
using TypeOEngine.Typedeaf.Core.Entities;

namespace TypeD.Commands.Project
{
    public partial class ProjectCommand
    {
        public static async Task<ProjectModel> Load(string projectFilePath)
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

                    foreach(var typeDType in project.TypeDTypes.Values)
                    {
                        if (typeDType.TypeInfo == null) continue;

                        switch (typeDType.TypeType)
                        {
                            case TypeDTypeType.Game:
                                project.AddCode(new GameCode(project));
                                project.AddCode(new GameTypeDCode(project));
                                break;
                            case TypeDTypeType.Scene:
                                project.AddCode(new SceneCode(project, typeDType.Name, typeDType.Namespace));
                                project.AddCode(new SceneTypeDCode(project, typeDType.Name, typeDType.Namespace));
                                break;
                            case TypeDTypeType.Entity:
                                project.AddCode(new EntityCode(project, typeDType.Name, typeDType.Namespace));
                                project.AddCode(new EntityTypeDCode(project, typeDType.Name, typeDType.Namespace));
                                break;
                            case TypeDTypeType.Stub:
                                break;
                            case TypeDTypeType.Logic:
                                break;
                            case TypeDTypeType.Drawable:
                                break;
                            case TypeDTypeType.EntityData:
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
