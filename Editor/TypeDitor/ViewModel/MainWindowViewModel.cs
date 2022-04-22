using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Data.SettingContexts;
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
        ISettingModel SettingModel { get; set; }

        // Commands
        public BuildProjectCommand BuildProjectCommand { get; set; }
        public ExitProjectCommand ExitProjectCommand { get; set; }
        public NewProjectCommand NewProjectCommand { get; set; }
        public ImportProjectCommand ImportProjectCommand { get; set; }
        public OpenProjectCommand OpenProjectCommand { get; set; }
        public RunProjectCommand RunProjectCommand { get; set; }
        public SaveProjectCommand SaveProjectCommand { get; set; }

        // Data
        public Project LoadedProject { get; private set; }

        // Properties
        public int SizeX
        {
            get
            {
                var setting = SettingModel.GetContext<MainWindowSettingContext>(SettingLevel.Local);
                return setting.SizeX.Value;
            }
            set
            {
                var setting = SettingModel.GetContext<MainWindowSettingContext>(SettingLevel.Local);
                setting.SizeX.Value = value;
                SettingModel.SetContext(setting);
            }
        }

        public int SizeY
        {
            get
            {
                var setting = SettingModel.GetContext<MainWindowSettingContext>(SettingLevel.Local);
                return setting.SizeY.Value;
            }
            set
            {
                var setting = SettingModel.GetContext<MainWindowSettingContext>(SettingLevel.Local);
                setting.SizeY.Value = value;
                SettingModel.SetContext(setting);
            }
        }

        public WindowState Maximized
        {
            get
            {
                var setting = SettingModel.GetContext<MainWindowSettingContext>(SettingLevel.Local);
                return setting.Fullscreen.Value ? WindowState.Maximized : WindowState.Normal;
            }
            set
            {
                var setting = SettingModel.GetContext<MainWindowSettingContext>(SettingLevel.Local);
                setting.Fullscreen.Value = value == WindowState.Maximized;
                SettingModel.SetContext(setting);
            }
        }

        // View
        public MainWindow MainWindow { get; set; }

        // Constructors
        public MainWindowViewModel(MainWindow mainWindow, Project loadedProject) : base(mainWindow)
        {
            LoadedProject = loadedProject;
            MainWindow = mainWindow;

            HookModel = ResourceModel.Get<IHookModel>();
            SaveModel = ResourceModel.Get<ISaveModel>();
            SettingModel = ResourceModel.Get<ISettingModel>();
            
            BuildProjectCommand = new BuildProjectCommand(mainWindow);
            ExitProjectCommand = new ExitProjectCommand(mainWindow);
            NewProjectCommand = new NewProjectCommand(mainWindow);
            ImportProjectCommand = new ImportProjectCommand(mainWindow);
            OpenProjectCommand = new OpenProjectCommand(mainWindow);
            RunProjectCommand = new RunProjectCommand(mainWindow);
            SaveProjectCommand = new SaveProjectCommand(mainWindow);

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
            var dialog = new ModulesDialog(LoadedProject);
            dialog.Show();
        }

        public void OpenOptionsWindow()
        {
            var dialog = new OptionsDialog(LoadedProject);
            dialog.Show();
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

            HookModel.Shoot(new ExitHook() { Project = LoadedProject });
            return false;
        }
    }
}
