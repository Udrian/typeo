using System.Windows.Controls;
using TypeD.Models.Data;
using TypeDitor.ViewModel.Panels;

namespace TypeDitor.View.Panels
{
    /// <summary>
    /// Interaction logic for ComponentPanel.xaml
    /// </summary>
    public partial class ComponentPanel : UserControl
    {
        // ViewModel
        ComponentViewModel ViewModel { get; set; }
        
        // Constructors
        public ComponentPanel(Project project)
        {
            DataContext = ViewModel = new ComponentViewModel(this, project);
            InitializeComponent();
        }
    }
}
