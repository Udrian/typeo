using TypeD.Models;
using TypeDCore.Code.Drawable2d;

namespace TypeDCore.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static void CreateDrawable2d(this TypeD.Commands.Project.ProjectCommand _, ProjectModel project, string className, string @namespace)
        {
            project.AddCode(new Drawable2dCode(project, className, $"{project.ProjectName}.{@namespace}"), "Drawable");

            project.BuildTree();
        }
    }
}
