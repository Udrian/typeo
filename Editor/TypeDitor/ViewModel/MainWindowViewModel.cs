using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.Commands.Project;

namespace TypeDitor.ViewModel
{
    class MainWindowViewModel
    {
        // Models
        private IProjectModel ProjectModel { get; set; }

        // Providers
        private IRecentProvider RecentProvider { get; set; }
        private IProjectProvider ProjectProvider { get; set; }

        // Commands
        public BuildProjectCommand BuildProjectCommand { get; set; }
        public ExitProjectCommand ExitProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public SaveProjectCommand SaveProjectCommand { get; set; }

        // Constructors
        public MainWindowViewModel(
                                            IProjectModel projectModel,
            IRecentProvider recentProvider, IProjectProvider projectProvider
        )
        {
            ProjectModel = projectModel;
            RecentProvider = recentProvider;
            ProjectProvider = projectProvider;

            BuildProjectCommand = new BuildProjectCommand(projectModel);
            ExitProjectCommand = new ExitProjectCommand();
            NewProjectCommand = new NewProjectCommand(RecentProvider, ProjectProvider);
            OpenProjectCommand = new OpenProjectCommand(RecentProvider, ProjectProvider);
            SaveProjectCommand = new SaveProjectCommand();
        }

        public Project LoadedProject { get; set; }
    }
}
