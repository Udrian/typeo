using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

namespace TypeDitor.ViewModel.Dialogs.Tools
{
    class ModulesDialogViewModel
    {
        public class Module
        {
            public string Name { get; set; }
            public bool Enabled { get; set; }
            public string Version { get; set; }
        }

        // Modules
        IModuleModel ModuleModel { get; set; }
        IProjectModel ProjectModel { get; set; }
        ISaveModel SaveModel { get; set; }

        // Providers
        IModuleProvider ModuleProvider { get; set; }

        // Data
        Project LoadedProject { get; set; }
        ObservableCollection<Module> Modules { get; set; }

        // Constructors
        public ModulesDialogViewModel(IModuleModel moduleModel, IProjectModel projectModel, ISaveModel saveModel, IModuleProvider moduleProvider, Project loadedProject)
        {
            ModuleModel = moduleModel;
            ProjectModel = projectModel;
            SaveModel = saveModel;
            ModuleProvider = moduleProvider;
            LoadedProject = loadedProject;
        }

        public async Task<ObservableCollection<Module>> ListModules()
        {
            var moduleList = await ModuleProvider.List();
            Modules = new ObservableCollection<Module>();
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
                    Version = enabledVersion
                });
            }

            return Modules;
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

            // TODO: Remove Modules

            foreach(var module in added)
            {
                var createdModule = ModuleProvider.Create(module.Name, module.Version);
                ProjectModel.AddModule(LoadedProject, createdModule);
                await ModuleModel.Download(createdModule);
                ModuleModel.LoadAssembly(createdModule);
                //TODO: Fix InitializeTypeD when downloading module
                //ModuleModel.InitializeTypeD(module);
            }
        }
    }
}
