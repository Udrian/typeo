using System.Windows.Controls;
using TypeD.Models.Data;
using TypeDCore.ViewModel.Viewer;

namespace TypeDCore.View.Viewer
{
    /// <summary>
    /// Interaction logic for ConsoleViewer.xaml
    /// </summary>
    public partial class ConsoleViewer : UserControl
    {
        ConsoleViewModel ConsoleViewModel { get; set; }

        public Component Component { get; set; }

        public ConsoleViewer(Project project, Component component)
        {
            InitializeComponent();

            Component = component;

            DataContext = ConsoleViewModel = new ConsoleViewModel(this, project, component);
        }

        private void ConsoleViewerUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ConsoleViewModel.Unload();
        }
    }
}
