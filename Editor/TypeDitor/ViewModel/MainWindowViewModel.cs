using System.Windows.Controls;
using System.Windows.Input;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.ViewModel;
using TypeDitor.Commands;
using TypeDitor.Helpers;
using TypeDitor.View;
using TypeDitor.View.Dialogs.Tools;

namespace TypeDitor.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        // Models
        IResourceModel ResourceModel { get; set; }
        IProjectModel ProjectModel { get; set; }
        IModuleModel ModuleModel { get; set; }
        IHookModel HookModel { get; set; }
        ISaveModel SaveModel { get; set; }
        IUINotifyModel UINotifyModel { get; set; }

        // Providers
        IRecentProvider RecentProvider { get; set; }
        IProjectProvider ProjectProvider { get; set; }
        IModuleProvider ModuleProvider { get; set; }

        // Commands
        public BuildProjectCommand BuildProjectCommand { get; set; }
        public ExitProjectCommand ExitProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public RunProjectCommand RunProjectCommand { get; set; }
        public SaveProjectCommand SaveProjectCommand { get; set; }
        public OpenComponentCommand OpenComponentCommand { get; set; }

        // Data
        public Project LoadedProject { get; private set; }
        
        // View
        MainWindow MainWindow { get; set; }

        // Constructors
        public MainWindowViewModel(IResourceModel resourceModel, Project loadedProject, MainWindow mainWindow)
        {
            ResourceModel = resourceModel;

            ProjectModel = ResourceModel.Get<IProjectModel>();
            ModuleModel = ResourceModel.Get<IModuleModel>();
            HookModel = ResourceModel.Get<IHookModel>();
            SaveModel = ResourceModel.Get<ISaveModel>();
            UINotifyModel = ResourceModel.Get<IUINotifyModel>();

            RecentProvider = ResourceModel.Get<IRecentProvider>();
            ProjectProvider = ResourceModel.Get<IProjectProvider>();
            ModuleProvider = ResourceModel.Get<IModuleProvider>();

            BuildProjectCommand = new BuildProjectCommand(ProjectModel, SaveModel);
            ExitProjectCommand = new ExitProjectCommand();
            NewProjectCommand = new NewProjectCommand(RecentProvider, ProjectProvider);
            OpenProjectCommand = new OpenProjectCommand(RecentProvider, ProjectProvider);
            RunProjectCommand = new RunProjectCommand(ProjectModel);
            SaveProjectCommand = new SaveProjectCommand(SaveModel);
            OpenComponentCommand = new OpenComponentCommand(this);

            LoadedProject = loadedProject;
            MainWindow = mainWindow;

            UINotifyModel.Attach("MainWindowViewModel", (name) => {
                CommandManager.InvalidateRequerySuggested(); //TODO: Maybe find a better way to get this notified
                OnPropertyChanged(name);
            });
        }

        // Functions
        public void InitUI(MainWindow mainWindow)
        {
            var initUIHook = new InitUIHook(LoadedProject);
            HookModel.Shoot(initUIHook);

            foreach(var menu in initUIHook.Menu.Items)
            {
                ViewHelper.InitMenu(mainWindow.TopMenu, menu, this);
            }
        }

        public void OpenModulesWindow()
        {
            var modulesDialog = new ModulesDialog(ModuleModel, ProjectModel, SaveModel, ModuleProvider, LoadedProject);
            modulesDialog.Show();
        }

        public void OpenDocument(string header, object content)
        {
            MainWindow.Tabs.Items.Add(new TabItem() { Header = header, Content = content });
        }
    }
}
