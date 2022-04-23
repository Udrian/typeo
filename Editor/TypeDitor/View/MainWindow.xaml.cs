using System.Windows;
using System.Windows.Controls;
using TypeD.Models.Data;
using TypeDitor.ViewModel;

namespace TypeDitor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ViewModel
        MainWindowViewModel ViewModel { get; set; }

        // Constructors
        public MainWindow(Project loadedProject)
        {
            InitializeComponent();

            ViewModel = new MainWindowViewModel(this, loadedProject);
            DataContext = ViewModel;

            DockRoot.AddPanel(ViewModel.TabsPanel);
            DockRoot.AddPanel(ViewModel.ComponentPanel, Dock.Left, 175, false);
            var outputPanelRoot = DockRoot.AddPanel(ViewModel.OutputPanel, Dock.Bottom, 250, true);
            outputPanelRoot.AddPanel(ViewModel.ComponentTypeBrowserPanel, Dock.Right, 175, true);

            ViewModel.InitUI(this);
        }

        public Menu TopMenu { get { return _TopMenu; } }

        private void ModulesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenModulesWindow();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = await ViewModel.OnClose();
        }

        private void OptionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenOptionsWindow();
        }
    }
}
