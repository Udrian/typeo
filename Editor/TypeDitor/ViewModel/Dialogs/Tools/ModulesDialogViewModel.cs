using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.ViewModel;

namespace TypeDitor.ViewModel.Dialogs.Tools
{
    class ModulesDialogViewModel : ViewModelBase
    {
        public class Module : ViewModelBase
        {
            public string Name { get; set; }
            public bool Enabled { get; set; }
            public string Version { get; set; }
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

        // Constructors
        public ModulesDialogViewModel(FrameworkElement element, TypeD.Models.Data.Project loadedProject) : base(element)
        {
            ModuleModel = ResourceModel.Get<IModuleModel>();
            ProjectModel = ResourceModel.Get<IProjectModel>();
            ModuleProvider = ResourceModel.Get<IModuleProvider>();
            LoadedProject = loadedProject;
            Modules = new ObservableCollection<Module>();
        }

        // Functions
        public async Task ListModules()
        {
            var moduleList = await ModuleProvider.List();
            Modules.Clear();
            foreach(var m1 in moduleList)
            {
                var enabled = false;
                var enabledVersion = m1.Versions.FirstOrDefault();
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
                    Progress = 0,
                    ProgressVisible = Visibility.Hidden
                });
            }
        }

        public async void Save()
        {
            var added = new List<Module>();
            var removed = new List<Module>();

            foreach (var module in Modules)
            {
                var found = false;
                foreach (var loadedModules in LoadedProject.Modules)
                {
                    if(module.Name == loadedModules.Name)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    if (!module.Enabled)
                    {
                        removed.Add(module);
                    }
                }
                else
                {
                    if(module.Enabled)
                    {
                        added.Add(module);
                    }
                }
            }

            foreach(var module in removed)
            {
                ProjectModel.RemoveModule(LoadedProject, module.Name);
            }

            foreach(var module in added)
            {
                module.ProgressVisible = Visibility.Visible;
                var createdModule = ModuleProvider.Create(module.Name, module.Version);
                ProjectModel.AddModule(LoadedProject, createdModule);
                await ModuleModel.Download(createdModule, (bytes, mProgress, totalBytes) => {
                    module.Progress = mProgress;
                    module.BytesDownloaded = bytes;
                    module.TotalBytesDownload = totalBytes;
                    module.OnPropertyChanged(nameof(module.DownloadText));
                });
                ModuleModel.LoadAssembly(createdModule);
            }
        }
    }
}
