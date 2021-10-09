using System.Collections.Generic;
using TypeD.Models;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeDitor.Commands.Project;

namespace TypeDitor.ViewModel
{
    class MainWindowViewModel
    {
        //Models
        private IRecentModel RecentModel { get; set; }

        //Commands
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }
        public SaveProjectCommand SaveProjectCommand { get; set; }
        public ExitProjectCommand ExitProjectCommand { get; set; }

        public MainWindowViewModel(IRecentModel recentModel)
        {
            RecentModel = recentModel;

            OpenProjectCommand = new OpenProjectCommand(RecentModel);
            NewProjectCommand = new NewProjectCommand(RecentModel);
            SaveProjectCommand = new SaveProjectCommand();
            ExitProjectCommand = new ExitProjectCommand();
        }

        public IList<Recent> GetRecents()
        {
            return RecentModel.Get();
        }

        public ProjectModel LoadedProject { get; set; }
    }
}
