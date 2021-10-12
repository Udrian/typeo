﻿using System.Linq;
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

        // Constructors
        public MainWindowViewModel(
                                            IProjectModel projectModel, IHookModel hookModel, ISaveModel saveModel,
            IRecentProvider recentProvider, IProjectProvider projectProvider
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
        }

        public void InitUI(MainWindow mainWindow)
        {
            var initUIHook = new InitUIHook(LoadedProject);
            HookModel.Shoot("InitUI", initUIHook);

            foreach(var menu in initUIHook.Menu.Items)
            {
                InitMenu(mainWindow.TopMenu, menu);
            }
        }

        private void InitMenu(System.Windows.Controls.ItemsControl currentMenu, MenuItem item)
        {
            var newMenuItem = new System.Windows.Controls.MenuItem() { Header = item.Name };
            if(item.Click != null)
            {
                newMenuItem.Click += (object sender, System.Windows.RoutedEventArgs e) =>
                {
                    object param = null;
                    if(!string.IsNullOrEmpty(item.ClickParameter))
                    {
                        var type = GetType();
                        param = type.GetProperties().FirstOrDefault(p => p.Name == item.ClickParameter).GetValue(this);
                    }
                    item.Click(param);
                };
            }
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
