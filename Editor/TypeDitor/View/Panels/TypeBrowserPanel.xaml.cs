using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.TreeNodes;
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

        // Data
        Project LoadedProject { get; set; }

        // Constructors
        public TypeBrowserPanel(Project loadedProject)
        {
            InitializeComponent();
            LoadedProject = loadedProject;

            TypeBrowserViewModel = new TypeBrowserViewModel(
                FindResource("HookModel") as IHookModel,
                LoadedProject,
                TreeView
            );
            DataContext = TypeBrowserViewModel;
        }

        private void ContextMenu_Opened(object sender, System.Windows.RoutedEventArgs e)
        {
            TypeBrowserViewModel.ContextMenuOpened(ContextMenu, TreeView.SelectedItem as Node);
        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem =
              VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
        }

        static T VisualUpwardSearch<T>(DependencyObject source) where T : DependencyObject
        {
            DependencyObject returnVal = source;

            while (returnVal != null && !(returnVal is T))
            {
                DependencyObject tempReturnVal = null;
                if (returnVal is Visual || returnVal is Visual3D)
                {
                    tempReturnVal = VisualTreeHelper.GetParent(returnVal);
                }
                if (tempReturnVal == null)
                {
                    returnVal = LogicalTreeHelper.GetParent(returnVal);
                }
                else returnVal = tempReturnVal;
            }

            return returnVal as T;
        }
    }
}
