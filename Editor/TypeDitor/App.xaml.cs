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
        
        // Providers
        public IRecentProvider RecentProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Models
            RecentModel = new RecentModel();

            var modelResource = new ResourceDictionary();
            modelResource.Add("RecentModel", RecentModel);
            Resources = modelResource;

            // Providers
            RecentProvider = new RecentProvider(RecentModel as RecentModel);

            var providerResource = new ResourceDictionary();
            providerResource.Add("RecentProvider", RecentProvider);
            Resources.MergedDictionaries.Add(providerResource);
        }
    }
}
