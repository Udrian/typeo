using System.Windows;
using TypeD.Models;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.Models.Providers;
using TypeDitor.Models;
using System.Collections;

namespace TypeDitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Models
        public IResourceModel ResourceModel { get; set; }
        public IModuleModel ModuleModel { get; set; }
        public IProjectModel ProjectModel { get; set; }
        public IHookModel HookModel { get; set; }
        public ISaveModel SaveModel { get; set; }
        public IUINotifyModel UINotifyModel { get; set; }
        public ILogModel LogModel { get; set; }
        public IRestoreModel RestoreModel { get; set; }
        public IComponentModel ComponentModel { get; set; }
        public ISettingModel SettingModel { get; set; }

        // Providers
        public IRecentProvider RecentProvider { get; set; }
        public IModuleProvider ModuleProvider { get; set; }
        public IProjectProvider ProjectProvider { get; set; }
        public IComponentProvider ComponentProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Models
            ResourceModel = new ResourceModel(Resources);
            ModuleModel = new ModuleModel();
            ProjectModel = new ProjectModel();
            HookModel = new HookModel();
            SaveModel = new SaveModel();
            UINotifyModel = new UINotifyModel();
            LogModel = new LogModel();
            RestoreModel = new RestoreModel();
            ComponentModel = new ComponentModel();
            SettingModel = new SettingModel();

            var modelResource = new ResourceDictionary
            {
                { "ResourceModel", ResourceModel },
                { "ModuleModel", ModuleModel },
                { "ProjectModel", ProjectModel },
                { "HookModel", HookModel },
                { "SaveModel", SaveModel },
                { "UINotifyModel", UINotifyModel },
                { "LogModel", LogModel },
                { "RestoreModel", RestoreModel },
                { "ComponentModel", ComponentModel },
                { "SettingModel", SettingModel }
            };
            Resources.MergedDictionaries.Add(modelResource);
            
            // Providers
            RecentProvider = new RecentProvider();
            ModuleProvider = new ModuleProvider();
            ProjectProvider = new ProjectProvider();
            ComponentProvider = new ComponentProvider();

            var providerResource = new ResourceDictionary
            {
                { "RecentProvider", RecentProvider },
                { "ProjectProvider", ProjectProvider },
                { "ModuleProvider", ModuleProvider },
                { "ComponentProvider", ComponentProvider }
            };
            Resources.MergedDictionaries.Add(providerResource);

            // Init
            foreach(DictionaryEntry resouce in modelResource)
            {
                var model = resouce.Value as IModel;
                model.Init(ResourceModel);
            }

            foreach (DictionaryEntry resouce in providerResource)
            {
                var provider = resouce.Value as IProvider;
                provider.Init(ResourceModel);
            }
        }
    }
}
