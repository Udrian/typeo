using System.Windows;
using TypeD.Models;
using TypeD.Models.Interfaces;

namespace TypeDitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IRecentModel RecentModel { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RecentModel = new RecentModel();

            var modelResource = new ResourceDictionary();
            modelResource.Add("RecentModel", RecentModel);
            Resources = modelResource;
        }
    }
}
