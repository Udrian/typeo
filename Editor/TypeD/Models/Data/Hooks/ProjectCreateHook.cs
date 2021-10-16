namespace TypeD.Models.Data.Hooks
{
    public class ProjectCreateHook
    {
        public Project Project { get; private set; }

        public ProjectCreateHook(Project project)
        {
            Project = project;
        }
    }
}
