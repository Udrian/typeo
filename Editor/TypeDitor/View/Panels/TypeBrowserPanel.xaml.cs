using System.Windows.Controls;
using TypeD.Models.Interfaces;
using TypeDitor.ViewModel.Panels;

namespace TypeDitor.View.Panels
{
    /// <summary>
    /// Interaction logic for TypeBrowserPanel.xaml
    /// </summary>
    public partial class TypeBrowserPanel : UserControl
    {
        // ViewModel
        TypeBrowserViewModel TypeBrowserViewModel { get; set; }

        // Constructors
        public TypeBrowserPanel()
        {
            InitializeComponent();

             TypeBrowserViewModel = new TypeBrowserViewModel(
                FindResource("HookModel") as IHookModel,
                TreeView
            );
            DataContext = TypeBrowserViewModel;
        }
    }
}
