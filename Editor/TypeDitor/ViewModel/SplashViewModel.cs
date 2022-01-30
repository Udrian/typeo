using System.Collections.Generic;
using System.Windows;
using TypeD.Models.Data;
using TypeD.Models.Providers.Interfaces;
using TypeD.ViewModel;
using TypeDitor.Commands;

namespace TypeDitor.ViewModel
{
    class SplashViewModel : ViewModelBase 
    {
        // Providers
        private IRecentProvider RecentProvider { get; set; }
        private IProjectProvider ProjectProvider { get; set; }

        // Commands
        public ImportProjectCommand ImportProjectCommand { get; set; }
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }

        // Constructors
        public SplashViewModel(FrameworkElement element) : base(element)
        {
            RecentProvider = ResourceModel.Get<IRecentProvider>();
            ProjectProvider = ResourceModel.Get<IProjectProvider>(); ;

            ImportProjectCommand = new ImportProjectCommand(RecentProvider, ProjectProvider);
            OpenProjectCommand = new OpenProjectCommand(RecentProvider, ProjectProvider);
            NewProjectCommand = new NewProjectCommand(RecentProvider, ProjectProvider);
        }

        // Functions
        public IEnumerable<Recent> GetRecents()
        {
            return RecentProvider.Get();
        }
    }
}
