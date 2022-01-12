using System.Windows;
using System.Windows.Controls;
using TypeD.Models.Interfaces;
using TypeDitor.ViewModel.Panels;

namespace TypeDitor.View.Panels
{
    /// <summary>
    /// Interaction logic for OutputPanel.xaml
    /// </summary>
    public partial class OutputPanel : UserControl
    {
        // ViewModel
        OutputViewModel OutputViewModel { get; set; }

        // Constructors
        public OutputPanel()
        {
            InitializeComponent();

            OutputViewModel = new OutputViewModel(
                FindResource("LogModel") as ILogModel
            );
            DataContext = OutputViewModel;
        }

        private void tbOutputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbOutputText.ScrollToEnd();
        }

        private void Panel_Unloaded(object sender, RoutedEventArgs e)
        {
            OutputViewModel.Unload();
        }
    }
}
