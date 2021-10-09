using TypeD.Models.Data;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.Commands.Project;

namespace TypeDitor.ViewModel
{
    class MainWindowViewModel
    {
        //Providers
        private IRecentProvider RecentProvider { get; set; }
        private IProjectProvider ProjectProvider { get; set; }

        //Commands
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }
        public SaveProjectCommand SaveProjectCommand { get; set; }
        public ExitProjectCommand ExitProjectCommand { get; set; }

        public MainWindowViewModel(IRecentProvider recentProvider, IProjectProvider projectProvider)
        {
            RecentProvider = recentProvider;
            ProjectProvider = projectProvider;

            OpenProjectCommand = new OpenProjectCommand(RecentProvider, ProjectProvider);
            NewProjectCommand = new NewProjectCommand(RecentProvider, ProjectProvider);
            SaveProjectCommand = new SaveProjectCommand();
            ExitProjectCommand = new ExitProjectCommand();
        }

        public Project LoadedProject { get; set; }
    }
}
