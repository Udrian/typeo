using System.Collections.Generic;
using TypeD.Models.Data;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.Commands.Project;

namespace TypeDitor.ViewModel
{
    class SplashViewModel
    {
        // Providers
        private IRecentProvider RecentProvider { get; set; }

        // Commands
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }

        public SplashViewModel(IRecentProvider recentProvider)
        {
            RecentProvider = recentProvider;

            OpenProjectCommand = new OpenProjectCommand(RecentProvider);
            NewProjectCommand = new NewProjectCommand(RecentProvider);
        }

        public IList<Recent> GetRecents()
        {
            return RecentProvider.Get();
        }
    }
}
