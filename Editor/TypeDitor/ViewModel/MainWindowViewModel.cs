using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.ViewModel;
using TypeDitor.Commands;
using TypeDitor.Helpers;
using TypeDitor.View;
using TypeDitor.View.Dialogs.Tools;

namespace TypeDitor.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        // Models
        IHookModel HookModel { get; set; }
        ISaveModel SaveModel { get; set; }

        // Commands
        public BuildProjectCommand BuildProjectCommand { get; set; }
        public ExitProjectCommand ExitProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }
        public ImportProjectCommand ImportProjectCommand { get; set; }
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public RunProjectCommand RunProjectCommand { get; set; }
        public SaveProjectCommand SaveProjectCommand { get; set; }
        public OpenComponentCommand OpenComponentCommand { get; set; }

        // Data
        public Project LoadedProject { get; private set; }
        
        // View
        public MainWindow MainWindow { get; set; }

        // Constructors
        public MainWindowViewModel(MainWindow mainWindow, Project loadedProject) : base(mainWindow)
        {
            LoadedProject = loadedProject;
            MainWindow = mainWindow;

            HookModel = ResourceModel.Get<IHookModel>();
            SaveModel = ResourceModel.Get<ISaveModel>();

            BuildProjectCommand = new BuildProjectCommand(mainWindow);
            ExitProjectCommand = new ExitProjectCommand(mainWindow);
            NewProjectCommand = new NewProjectCommand(mainWindow);
            ImportProjectCommand = new ImportProjectCommand(mainWindow);
            OpenProjectCommand = new OpenProjectCommand(mainWindow);
            RunProjectCommand = new RunProjectCommand(mainWindow);
            SaveProjectCommand = new SaveProjectCommand(mainWindow);
            OpenComponentCommand = new OpenComponentCommand(this);

            UINotifyModel.Attach<MainWindowViewModel>((name) => {
                CommandManager.InvalidateRequerySuggested(); //TODO: Maybe find a better way to get this notified
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
            var modulesDialog = new ModulesDialog(LoadedProject);
            modulesDialog.Show();
        }

        public void OpenDocument(string header, object content)
        {
            MainWindow.Tabs.Items.Add(new TabItem() { Header = header, Content = content });
        }

        public async Task<bool> OnClose()
        {
            LoadedProject.IsClosing = true;
            if (SaveModel.AnythingToSave)
            {
                var result = MessageBox.Show("Save before closing?", "Closing...", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await SaveModel.Save();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    LoadedProject.IsClosing = false;
                    return true;
                }
            }
            return false;
        }
    }
}
