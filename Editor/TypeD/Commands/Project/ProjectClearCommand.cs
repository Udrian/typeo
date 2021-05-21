using TypeD.Models;

namespace TypeD.Commands.Project
{
    public partial class ProjectCommand
    {
        public void Clear(ProjectModel project)
        {
            if (project == null) return;

            project.Tree.Clear();
        }
    }
}
