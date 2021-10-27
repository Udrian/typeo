using System.Windows;
using TypeD.Models;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.Models.Providers;
using TypeDitor.Models;

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
        public IHookModel HookModel { get; set; }
        public IResourceModel ResourceModel { get; set; }
        public ISaveModel SaveModel { get; set; }

        // Providers
        public IRecentProvider RecentProvider { get; set; }
        public IModuleProvider ModuleProvider { get; set; }
        public IProjectProvider ProjectProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Models
            RecentModel = new RecentModel();
            HookModel = new HookModel();
            ResourceModel = new ResourceModel();
            SaveModel = new SaveModel();
            ModuleModel = new ModuleModel(HookModel, ResourceModel, SaveModel);
            ProjectModel = new ProjectModel(ModuleModel, HookModel, SaveModel, null);//TODO: Not really the way to go 1/2

            var modelResource = new ResourceDictionary
            {
                { "RecentModel", RecentModel },
                { "ModuleModel", ModuleModel },
                { "ProjectModel", ProjectModel },
                { "HookModel", HookModel },
                { "ResourceModel", ResourceModel },
                { "SaveModel", SaveModel }
            };
            Resources = modelResource;

            // Providers
            RecentProvider = new RecentProvider(RecentModel);
            ModuleProvider = new ModuleProvider(ModuleModel);
            ProjectProvider = new ProjectProvider(ProjectModel, ModuleModel, HookModel, SaveModel, ModuleProvider);
            (ProjectModel as ProjectModel).ProjectProvider = ProjectProvider;//TODO: Not really the way to go 2/2

            var providerResource = new ResourceDictionary
            {
                { "RecentProvider", RecentProvider },
                { "ProjectProvider", ProjectProvider },
                { "ModuleProvider", ModuleProvider }
            };
            Resources.MergedDictionaries.Add(providerResource);

            (ResourceModel as ResourceModel).Resources = Resources;
        }
    }
}
