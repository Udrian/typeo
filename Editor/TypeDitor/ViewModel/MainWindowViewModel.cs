using TypeD.Models;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.Commands.Project;

namespace TypeDitor.ViewModel
{
    class MainWindowViewModel
    {
        //Providers
        private IRecentProvider RecentProvider { get; set; }

        //Commands
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }
        public SaveProjectCommand SaveProjectCommand { get; set; }
        public ExitProjectCommand ExitProjectCommand { get; set; }

        public MainWindowViewModel(IRecentProvider recentProvider)
        {
            RecentProvider = recentProvider;

            OpenProjectCommand = new OpenProjectCommand(RecentProvider);
            NewProjectCommand = new NewProjectCommand(RecentProvider);
            SaveProjectCommand = new SaveProjectCommand();
            ExitProjectCommand = new ExitProjectCommand();
        }

        public ProjectModel LoadedProject { get; set; }
    }
}
