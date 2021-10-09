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
        public IProjectModel ProjectModel { get; set; }
        
        // Providers
        public IRecentProvider RecentProvider { get; set; }
        public IProjectProvider ProjectProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Models
            RecentModel = new RecentModel();
            ProjectModel = new ProjectModel();

            var modelResource = new ResourceDictionary();
            modelResource.Add("RecentModel", RecentModel);
            modelResource.Add("ProjectModel", ProjectModel);
            Resources = modelResource;

            // Providers
            RecentProvider = new RecentProvider(RecentModel);
            ProjectProvider = new ProjectProvider(ProjectModel);

            var providerResource = new ResourceDictionary();
            providerResource.Add("RecentProvider", RecentProvider);
            providerResource.Add("ProjectProvider", ProjectProvider);
            Resources.MergedDictionaries.Add(providerResource);
        }
    }
}
