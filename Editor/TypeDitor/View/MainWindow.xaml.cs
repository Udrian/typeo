using System.Windows;
using System.Windows.Controls;
using TypeD.Models.Data;
using TypeDitor.View.Panels;
using TypeDitor.ViewModel;

namespace TypeDitor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ViewModel
        MainWindowViewModel MainWindowViewModel { get; set; }

        public TabControl Tabs { get; set; }

        // Constructors
        public MainWindow(Project loadedProject)
        {
            InitializeComponent();

            MainWindowViewModel = new MainWindowViewModel(this, loadedProject);
            DataContext = MainWindowViewModel;

            Application.Current.Resources.Add("MainWindowViewModel", MainWindowViewModel);

            Tabs = new TabControl();

            DockRoot.AddPanel(new ComponentBrowserPanel(loadedProject), Dock.Left);
            DockRoot.AddPanel(new OutputPanel(), Dock.Bottom);
            DockRoot.AddPanel(Tabs);

            MainWindowViewModel.InitUI(this);
        }

        public Menu TopMenu { get { return _TopMenu; } }

        private void ModulesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.OpenModulesWindow();
        }
    }
}
