using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.Commands.Project;
using TypeDitor.Helpers;
using TypeDitor.View;

namespace TypeDitor.ViewModel
{
    class MainWindowViewModel
    {
        // Models
        private IProjectModel ProjectModel { get; set; }
        private IHookModel HookModel { get; set; }
        private ISaveModel SaveModel { get; set; }

        // Providers
        private IRecentProvider RecentProvider { get; set; }
        private IProjectProvider ProjectProvider { get; set; }

        // Commands
        public BuildProjectCommand BuildProjectCommand { get; set; }
        public ExitProjectCommand ExitProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public RunProjectCommand RunProjectCommand { get; set; }
        public SaveProjectCommand SaveProjectCommand { get; set; }

        // Data
        public Project LoadedProject { get; private set; }

        // Constructors
        public MainWindowViewModel(
                                            IProjectModel projectModel, IHookModel hookModel, ISaveModel saveModel,
            IRecentProvider recentProvider, IProjectProvider projectProvider,
            Project loadedProject
        )
        {
            ProjectModel = projectModel;
            HookModel = hookModel;
            SaveModel = saveModel;
            RecentProvider = recentProvider;
            ProjectProvider = projectProvider;

            BuildProjectCommand = new BuildProjectCommand(ProjectModel, SaveModel);
            ExitProjectCommand = new ExitProjectCommand();
            NewProjectCommand = new NewProjectCommand(RecentProvider, ProjectProvider);
            OpenProjectCommand = new OpenProjectCommand(RecentProvider, ProjectProvider);
            RunProjectCommand = new RunProjectCommand(ProjectModel);
            SaveProjectCommand = new SaveProjectCommand(SaveModel);

            LoadedProject = loadedProject;
        }

        public void InitUI(MainWindow mainWindow)
        {
            var initUIHook = new InitUIHook(LoadedProject);
            HookModel.Shoot("InitUI", initUIHook);

            foreach(var menu in initUIHook.Menu.Items)
            {
                ViewHelper.InitMenu(mainWindow.TopMenu, menu, this);
            }
        }
    }
}
