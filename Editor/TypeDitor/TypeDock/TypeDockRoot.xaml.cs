using System.Windows;
using System.Windows.Controls;

namespace TypeDitor.TypeDock
{
    /// <summary>
    /// Interaction logic for TypeDockRoot.xaml
    /// </summary>
    public partial class TypeDockRoot : UserControl
    {
        public TypeDockRoot()
        {
            InitializeComponent();
        }

        public void AddPanel(UIElement ui, Dock dock)
        {
            DockPanel.SetDock(ui, dock);
            DockRoot.Children.Add(ui);
        }
    }
}
