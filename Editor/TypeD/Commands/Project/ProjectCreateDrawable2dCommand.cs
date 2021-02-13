using TypeD.Code;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public partial class ProjectCommand
    {
        public static void CreateDrawable2d(ProjectModel project, string className, string @namespace)
        {
            project.AddCode(new Drawable2dCode(project, className, $"{project.ProjectName}.{@namespace}"));

            project.BuildTree();
        }
    }
}
