using System;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.View;
using TypeDitor.Commands.Project;
using TypeDitor.View;

namespace TypeDitor.ViewModel
{
    class MainWindowViewModel
    {
        // Models
        private IProjectModel ProjectModel { get; set; }
        private IHookModel HookModel { get; set; }

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

        // Constructors
        public MainWindowViewModel(
                                            IProjectModel projectModel, IHookModel hookModel,
            IRecentProvider recentProvider, IProjectProvider projectProvider
        )
        {
            ProjectModel = projectModel;
            HookModel = hookModel;
            RecentProvider = recentProvider;
            ProjectProvider = projectProvider;

            BuildProjectCommand = new BuildProjectCommand(ProjectModel);
            ExitProjectCommand = new ExitProjectCommand();
            NewProjectCommand = new NewProjectCommand(RecentProvider, ProjectProvider);
            OpenProjectCommand = new OpenProjectCommand(RecentProvider, ProjectProvider);
            RunProjectCommand = new RunProjectCommand(ProjectModel);
            SaveProjectCommand = new SaveProjectCommand();
        }

        public void InitUI(MainWindow mainWindow)
        {
            var initUIHook = new InitUIHook();
            HookModel.Shoot("InitUI", initUIHook);

            foreach(var menu in initUIHook.Menu.Items)
            {
                InitMenu(mainWindow.TopMenu, menu);
            }
        }

        private void InitMenu(System.Windows.Controls.ItemsControl currentMenu, MenuItem item)
        {
            var newMenuItem = new System.Windows.Controls.MenuItem() { Header = item.Name };
            currentMenu.Items.Add(newMenuItem);
            currentMenu = newMenuItem;

            foreach (var menu in item.Items)
            {
                InitMenu(currentMenu, menu);
            }
        }

        public Project LoadedProject { get; set; }
    }
}
