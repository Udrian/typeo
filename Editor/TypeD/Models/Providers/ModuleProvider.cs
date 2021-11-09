using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TypeD.Helpers;
using TypeD.Models.DTO;
using System.Threading.Tasks;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

namespace TypeD.Models.Providers
{
    public class ModuleProvider : IModuleProvider
    {
        // Models
        private ModuleModel ModuleModel { get; set; }

        // Constructors
        public ModuleProvider(IModuleModel moduleModel)
        {
            ModuleModel = moduleModel as ModuleModel;
        }

        // Functions
        public async Task<IEnumerable<ModuleList>> List()
        {
#if DEBUG
            return await Task.Run(() =>
            {
                return new List<ModuleList>(){
                    new ModuleList() { Name = "TypeOCore", Versions = new List<string>() { "local", "local" } },
                    new ModuleList() { Name = "TypeDCore", Versions = new List<string>() { "local", "local" } },
                    new ModuleList() { Name = "TypeOBasic2d", Versions = new List<string>() { "local", "local" } },
                    new ModuleList() { Name = "TypeODesktop", Versions = new List<string>() { "local", "local" } },
                    new ModuleList() { Name = "TypeOSDL", Versions = new List<string>() { "local", "local" } }
                };
            });
#else
            var data = await APICaller.GetJsonObject<ModuleListDTO>("module/list");
            var moduleList = data.Modules.Select(m => { return new ModuleList() { Name = m.Key, Versions = m.Value }; });
            return moduleList;
#endif
        }

        public Module Create(string name, string version)
        {
            var module = new Module() { Name = name, Version = version };

            return module;
        }
    }
}
