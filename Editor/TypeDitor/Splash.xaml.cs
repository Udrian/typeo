using System.Windows;
using System.Windows.Controls;
using TypeD.Models;
using TypeDitor.Commands;

namespace TypeDitor
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        public Splash()
        {
            InitializeComponent();

            RecentList.ItemsSource = RecentModel.LoadRecents();
        }

        private void RecentList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var lbRecent = sender as ListBox;
            if (lbRecent == null) return;
            var recent = lbRecent.SelectedItem as RecentModel;
            if (recent == null) return;
            ProjectCommands.OpenProject.Execute(recent);
        }
    }
}
