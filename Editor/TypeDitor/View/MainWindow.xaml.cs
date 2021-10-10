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
        MainWindowViewModel MainWindowViewModel { get; set; }

        public MainWindow(Project loadedProject)
        {
            InitializeComponent();

            MainWindowViewModel = new MainWindowViewModel(
                FindResource("ProjectModel") as IProjectModel, FindResource("HookModel") as IHookModel,
                FindResource("RecentProvider") as IRecentProvider, FindResource("ProjectProvider") as IProjectProvider
            );
            MainWindowViewModel.LoadedProject = loadedProject;
            DataContext = MainWindowViewModel;

            DockRoot.AddPanel(new EmptyDocument());
            DockRoot.AddPanel(new OutputPanel(), Dock.Bottom);
            DockRoot.AddPanel(new EmptyDocument(), Dock.Left);

            MainWindowViewModel.InitUI(this);
        }

        public Menu TopMenu { get { return _TopMenu; } }
    }
}
