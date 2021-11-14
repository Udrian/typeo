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
        public ITypeOTypeProvider TypeOTypeProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            // Models
            ResourceModel = new ResourceModel(Resources);
            RecentModel = new RecentModel();
            HookModel = new HookModel();
            SaveModel = new SaveModel();
            ModuleModel = new ModuleModel();
            ProjectModel = new ProjectModel();

            var modelResource = new ResourceDictionary
            {
                { "RecentModel", RecentModel },
                { "ModuleModel", ModuleModel },
                { "ProjectModel", ProjectModel },
                { "HookModel", HookModel },
                { "ResourceModel", ResourceModel },
                { "SaveModel", SaveModel }
            };
            Resources.MergedDictionaries.Add(modelResource);
            
            // Providers
            RecentProvider = new RecentProvider();
            ModuleProvider = new ModuleProvider();
            ProjectProvider = new ProjectProvider();
            TypeOTypeProvider = new TypeOTypeProvider();

            var providerResource = new ResourceDictionary
            {
                { "RecentProvider", RecentProvider },
                { "ProjectProvider", ProjectProvider },
                { "ModuleProvider", ModuleProvider },
                { "TypeOTypeProvider", TypeOTypeProvider }
            };
            Resources.MergedDictionaries.Add(providerResource);

            // Init
            foreach(IModelProvider resouce in modelResource)
            {
                resouce.Init(ResourceModel);
            }

            foreach (IModelProvider resouce in providerResource)
            {
                resouce.Init(ResourceModel);
            }
        }
    }
}
