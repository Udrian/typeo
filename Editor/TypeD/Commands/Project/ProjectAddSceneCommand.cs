using TypeD.Code;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public partial class ProjectCommand
    {
        public static void AddScene(ProjectModel project, string className, string @namespace)
        {
            project.AddCode(new SceneCode(project, className, $"{project.ProjectName}.{@namespace}"));
            project.AddCode(new SceneTypeDCode(project, className, $"{project.ProjectName}.{@namespace}"));

            project.BuildTree();
        }
    }
}
