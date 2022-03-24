using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.DTO;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

namespace TypeD.Models.Providers
{
    public class ModuleProvider : IModuleProvider, IProvider
    {
        // Models
        IResourceModel ResourceModel { get; set; }

        // Constructors
        public ModuleProvider()
        {
        }

        public void Init(IResourceModel resourceModel)
        {
            ResourceModel = resourceModel;
        }

        // Functions
        public async Task<IEnumerable<ModuleList>> List(Project project)
        {
            List<ModuleList> moduleList = new List<ModuleList>();

            var data = await APICaller.GetJsonObject<ModuleListDTO>("module/list");
            if(data?.Modules != null)
            {
                moduleList.AddRange(data.Modules.Select(m => { return new ModuleList() { Name = m.Key, Versions = m.Value }; }).ToList());
            }

#if DEBUG
            var devModules = new List<ModuleList>(){
                new ModuleList() { Name = "TypeOCore", Versions = new List<string>() { "local" } },
                new ModuleList() { Name = "TypeDCore", Versions = new List<string>() { "local" } },
                new ModuleList() { Name = "TypeOBasic2d", Versions = new List<string>() { "local" } },
                new ModuleList() { Name = "TypeODesktop", Versions = new List<string>() { "local" } },
                new ModuleList() { Name = "TypeOSDL", Versions = new List<string>() { "local" } },
                new ModuleList() { Name = "TypeDSDL", Versions = new List<string>() { "local" } }
            };
            foreach (var module in devModules)
            {
                var existingModule = moduleList.Find(m => m.Name == module.Name);
                if (existingModule == null)
                {
                    existingModule = new ModuleList() { Name = module.Name, Versions = new List<string>() };
                    moduleList.Add(existingModule);
                }
                foreach(var moduleVersion in module.Versions)
                {
                    if (!existingModule.Versions.Exists(v => v == moduleVersion))
                    {
                        existingModule.Versions.Add(moduleVersion);
                    }
                }
            }
#endif

            foreach (var module in project.Modules)
            {
                var existingModule = moduleList.Find(m => m.Name == module.Name);
                if (existingModule == null)
                {
                    moduleList.Add(new ModuleList() { Name = module.Name, Versions = new List<string>() { module.Version } });
                }
                else
                {
                    if(!existingModule.Versions.Exists(v => v == module.Version))
                    {
                        existingModule.Versions.Add(module.Version);
                    }
                }
            }
            return moduleList;

        }

        public Module Create(string name, string version)
        {
            var module = new Module() { Name = name, Version = version };

            return module;
        }
    }
}
