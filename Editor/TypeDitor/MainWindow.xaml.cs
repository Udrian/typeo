using System.Windows;
using TypeD.Models;

namespace TypeDitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ProjectModel loadedProject)
        {
            this.DataContext = this;
            this.LoadedProject = loadedProject;
            InitializeComponent();
        }

        public ProjectModel LoadedProject { get; set; }
    }
}
