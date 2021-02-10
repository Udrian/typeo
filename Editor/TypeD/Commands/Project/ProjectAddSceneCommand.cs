using TypeD.Code;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static void AddScene(ProjectModel project, string className, string @namespace)
        {
            project.AddCode(new SceneCode(project, className, @namespace));
            project.AddCode(new SceneTypeDCode(project, className, @namespace));
        }
    }
}
