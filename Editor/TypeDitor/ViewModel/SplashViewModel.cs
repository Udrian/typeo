using System.Collections.Generic;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeDitor.Commands.Project;

namespace TypeDitor.ViewModel
{
    class SplashViewModel
    {
        //Models
        private IRecentModel RecentModel { get; set; }

        //Commands
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }

        public SplashViewModel(IRecentModel recentModel)
        {
            RecentModel = recentModel;

            OpenProjectCommand = new OpenProjectCommand(RecentModel);
            NewProjectCommand = new NewProjectCommand(RecentModel);
        }

        public IList<Recent> GetRecents()
        {
            return RecentModel.Get();
        }
    }
}
