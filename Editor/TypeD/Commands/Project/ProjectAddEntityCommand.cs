using TypeD.Code;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static void AddEntity(ProjectModel project, string className, string @namespace)
        {
            project.AddCode(new EntityCode(project, className, @namespace));
            project.AddCode(new EntityTypeDCode(project, className, @namespace));
        }
    }
}
