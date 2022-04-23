using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.ViewModel;

namespace TypeDitor.ViewModel.Dialogs.Tools
{
    internal class ModulesDialogViewModel : ViewModelBase
    {
        public class Module : ViewModelBase
        {
            public string Name { get; set; }
            public bool Enabled { get; set; }
            public string Version { get; set; }
            public List<string> Versions { get; set; } = new List<string>();
            private int progress;
            public int Progress { 
                get => progress;
                set
                {
                    progress = value;
                    OnPropertyChanged();
                }
            }
            private Visibility progressVisible;
            public Visibility ProgressVisible {
                get => progressVisible;
                set
                {
                    progressVisible = value;
                    OnPropertyChanged();
                }
            }
            public long BytesDownloaded { get; set; }
            public long TotalBytesDownload { get; set; }
            public string DownloadText { get { return $"{BytesDownloaded}/{TotalBytesDownload} ({Progress}%)"; } }
        }

        // Modules
        IModuleModel ModuleModel { get; set; }
        IProjectModel ProjectModel { get; set; }

        // Providers
        IModuleProvider ModuleProvider { get; set; }

        // Data
        TypeD.Models.Data.Project LoadedProject { get; set; }
        public ObservableCollection<Module> Modules { get; set; }

        // Properties
        public Module SelectedModule { get; set; }
        public Visibility SelectedModuleVisibility { get { return SelectedModule == null ? Visibility.Hidden : Visibility.Visible; } }

        // Constructors
        public ModulesDialogViewModel(FrameworkElement element, TypeD.Models.Data.Project loadedProject) : base(element)
        {
            Modules = new ObservableCollection<Module>();
            LoadedProject = loadedProject;

            ModuleModel = ResourceModel.Get<IModuleModel>();
            ProjectModel = ResourceModel.Get<IProjectModel>();
            ModuleProvider = ResourceModel.Get<IModuleProvider>();
        }

        // Functions
        public async Task ListModules()
        {
            var moduleList = await ModuleProvider.List(LoadedProject);
            Modules.Clear();
            foreach(var m1 in moduleList)
            {
                var enabled = false;
                var enabledVersion = "";
                foreach(var m2 in LoadedProject.Modules)
                {
                    if(m1.Name == m2.Name)
                    {
                        enabled = true;
                        enabledVersion = m2.Version;
                    }
                }
                Modules.Add(new Module()
                {
                    Name = m1.Name,
                    Enabled = enabled,
                    Version = enabledVersion,
                    Versions = m1.Versions,
                    Progress = 0,
                    ProgressVisible = Visibility.Hidden
                });
            }
        }

        public async void InstallSelectedModule()
        {
            if (SelectedModule == null || string.IsNullOrEmpty(SelectedModule.Version)) return;
            var module = SelectedModule;
            module.ProgressVisible = Visibility.Visible;
            var createdModule = ModuleProvider.Create(module.Name, module.Version);
            ProjectModel.AddModule(LoadedProject, createdModule);
            await ModuleModel.Download(createdModule, (bytes, mProgress, totalBytes) => {
                module.Progress = mProgress;
                module.BytesDownloaded = bytes;
                module.TotalBytesDownload = totalBytes;
                module.OnPropertyChanged(nameof(module.DownloadText));
            });

            module.Progress = 0;
            module.BytesDownloaded = 0;
            module.TotalBytesDownload = 0;
            module.ProgressVisible = Visibility.Hidden;
            module.OnPropertyChanged(nameof(module.DownloadText));

            ModuleModel.LoadAssembly(createdModule);
            module.Enabled = true;
            module.OnPropertyChanged(nameof(module.Enabled));
            module.OnPropertyChanged(nameof(module.Version));
        }

        public void UninstallSelectedModule()
        {
            if (SelectedModule == null || !SelectedModule.Enabled) return;
            ProjectModel.RemoveModule(LoadedProject, SelectedModule.Name);
            SelectedModule.Enabled = false;
            SelectedModule.OnPropertyChanged(nameof(SelectedModule.Enabled));
            SelectedModule.Version = "";
            SelectedModule.OnPropertyChanged(nameof(SelectedModule.Version));
        }

        public void SelectedChanged(Module module)
        {
            SelectedModule = module;
            OnPropertyChanged(nameof(SelectedModule));
            OnPropertyChanged(nameof(SelectedModuleVisibility));
        }
    }
}
