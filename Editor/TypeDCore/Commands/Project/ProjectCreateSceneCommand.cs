using TypeD.Models;
using TypeDCore.Code.Scene;

namespace TypeDCore.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static void CreateScene(this TypeD.Commands.Project.ProjectCommand _, ProjectModel project, string className, string @namespace)
        {
            project.AddCode(new SceneCode(project, className, $"{project.ProjectName}.{@namespace}"), "Scene");
            project.AddCode(new SceneTypeDCode(project, className, $"{project.ProjectName}.{@namespace}"), "Scene");

            project.BuildTree();
        }
    }
}
