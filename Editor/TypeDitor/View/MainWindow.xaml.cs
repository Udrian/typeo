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
                FindResource("ProjectModel") as IProjectModel, FindResource("HookModel") as IHookModel, FindResource("SaveModel") as ISaveModel,
                FindResource("RecentProvider") as IRecentProvider, FindResource("ProjectProvider") as IProjectProvider,
                loadedProject
            );
            DataContext = MainWindowViewModel;

            DockRoot.AddPanel(new TypeBrowserPanel(loadedProject), Dock.Left);
            DockRoot.AddPanel(new OutputPanel(), Dock.Bottom);
            DockRoot.AddPanel(new EmptyDocument());

            MainWindowViewModel.InitUI(this);
        }

        public Menu TopMenu { get { return _TopMenu; } }
    }
}
