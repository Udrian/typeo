using System.Collections.Generic;
using TypeD.Models.Data;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.Commands;

namespace TypeDitor.ViewModel
{
    class SplashViewModel
    {
        // Providers
        private IRecentProvider RecentProvider { get; set; }
        private IProjectProvider ProjectProvider { get; set; }

        // Commands
        public ImportProjectCommand ImportProjectCommand { get; set; }
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }

        public SplashViewModel(IRecentProvider recentProvider, IProjectProvider projectProvider)
        {
            RecentProvider = recentProvider;
            ProjectProvider = projectProvider;

            ImportProjectCommand = new ImportProjectCommand(RecentProvider, ProjectProvider);
            OpenProjectCommand = new OpenProjectCommand(RecentProvider, ProjectProvider);
            NewProjectCommand = new NewProjectCommand(RecentProvider, ProjectProvider);
        }

        public IEnumerable<Recent> GetRecents()
        {
            return RecentProvider.Get();
        }
    }
}
