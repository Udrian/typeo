using TypeD.Code;
using TypeD.Data;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public partial class ProjectCommand
    {
        public static void CreateScene(ProjectModel project, string className, string @namespace)
        {
            project.AddCode(new SceneCode(project, className, $"{project.ProjectName}.{@namespace}"), TypeDTypeType.Scene);
            project.AddCode(new SceneTypeDCode(project, className, $"{project.ProjectName}.{@namespace}"), TypeDTypeType.Scene);

            project.BuildTree();
        }
    }
}
