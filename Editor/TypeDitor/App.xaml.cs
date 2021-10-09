using System.Windows;
using TypeD.Models;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.Models.Providers;

namespace TypeDitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Models
        public IRecentModel RecentModel { get; set; }
        public IModuleModel ModuleModel { get; set; }
        public IProjectModel ProjectModel { get; set; }
        
        // Providers
        public IRecentProvider RecentProvider { get; set; }
        public IModuleProvider ModuleProvider { get; set; }
        public IProjectProvider ProjectProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Models
            RecentModel = new RecentModel();
            ModuleModel = new ModuleModel();
            ProjectModel = new ProjectModel(ModuleModel);

            var modelResource = new ResourceDictionary();
            modelResource.Add("RecentModel", RecentModel);
            modelResource.Add("ModuleModel", ModuleModel);
            modelResource.Add("ProjectModel", ProjectModel);
            Resources = modelResource;

            // Providers
            RecentProvider = new RecentProvider(RecentModel);
            ModuleProvider = new ModuleProvider(ModuleModel);
            ProjectProvider = new ProjectProvider(ProjectModel, ModuleModel, ModuleProvider);

            var providerResource = new ResourceDictionary();
            providerResource.Add("RecentProvider", RecentProvider);
            providerResource.Add("ProjectProvider", ProjectProvider);
            Resources.MergedDictionaries.Add(providerResource);
        }
    }
}
