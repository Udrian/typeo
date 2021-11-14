using System.Windows;
using System.Windows.Controls;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.View.Documents;
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

        // Constructors
        public MainWindow(Project loadedProject)
        {
            InitializeComponent();

            MainWindowViewModel = new MainWindowViewModel(
                FindResource("ResourceModel") as IResourceModel,
                loadedProject
            );
            DataContext = MainWindowViewModel;

            DockRoot.AddPanel(new TypeBrowserPanel(loadedProject), Dock.Left);
            DockRoot.AddPanel(new OutputPanel(), Dock.Bottom);
            DockRoot.AddPanel(new EmptyDocument());

            MainWindowViewModel.InitUI(this);
        }

        public Menu TopMenu { get { return _TopMenu; } }

        private void ModulesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.OpenModulesWindow();
        }
    }
}
