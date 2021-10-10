﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.DTO;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

namespace TypeD.Models.Providers
{
    public class ModuleProvider : IModuleProvider
    {
        public ObservableCollection<Module> Modules { get; set; }

        // Models
        private ModuleModel ModuleModel { get; set; }

        // Constructors
        public ModuleProvider(IModuleModel moduleModel)
        {
            ModuleModel = moduleModel as ModuleModel;
            Modules = new ObservableCollection<Module>();
        }

        // Functions
        public async Task<IEnumerable<ModuleList>> List()
        {
            var data = await APICaller.GetJsonObject<ModuleListDTO>("module/list");
            var moduleList = data.Modules.Select(m => { return new ModuleList() { Name = m.Key, Versions = m.Value }; });
            return moduleList;
        }

        public Module Add(string name, string version)
        {
            var module = new Module() { Name = name, Version = version };

            ModuleModel.LoadAssembly(module);

            return module;
        }
    }
}