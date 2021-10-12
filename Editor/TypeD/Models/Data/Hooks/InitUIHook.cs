using TypeD.View;

namespace TypeD.Models.Data.Hooks
{
    public class InitUIHook
    {
        public Project Project { get; private set; }
        public Menu Menu { get; set; }

        public InitUIHook(Project project)
        {
            Menu = new Menu();
            Project = project;
        }
    }
}
