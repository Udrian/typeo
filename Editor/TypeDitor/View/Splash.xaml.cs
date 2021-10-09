using System.Windows;
using System.Windows.Controls;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeDitor.ViewModel;

namespace TypeDitor.View
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        SplashViewModel SplashViewModel { get; set; }

        public Splash()
        {
            InitializeComponent();
            SplashViewModel = new SplashViewModel(FindResource("RecentModel") as IRecentModel);
            DataContext = SplashViewModel;

            RecentList.ItemsSource = SplashViewModel.GetRecents();
        }

        private void RecentList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not ListBox lbRecent) return;
            if (lbRecent.SelectedItem is not Recent recent) return;
            SplashViewModel.OpenProjectCommand.Execute(recent);
        }
    }
}
