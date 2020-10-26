using TypeD.Models;

namespace TypeD.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static void Save(ProjectModel project)
        {
            project.Save();
        }
    }
}