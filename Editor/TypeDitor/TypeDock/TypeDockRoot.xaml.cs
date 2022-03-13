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

        public void AddPanel(UIElement ui, Dock? dock = null, int? length = null, bool span = false)
        {
            var leftright = false;
            var createGridSpliter = true;

            int column = -1;
            int row = -1;
            switch (dock)
            {
                case Dock.Left:
                    leftright = true;
                    column = 0;

                    break;
                case Dock.Top:
                    leftright = false;
                    row = 0;

                    break;
                case Dock.Right:
                    leftright = true;
                    column = 4;

                    break;
                case Dock.Bottom:
                    leftright = false;
                    row = 4;

                    break;
            }
            if(dock == null)
            {
                createGridSpliter = false;

                Grid.SetColumn(ui, 2);
                Grid.SetRow(ui, 2);
            }
            else
            {
                if(leftright)
                {
                    Grid.SetColumn(ui, column);
                    DockRoot.ColumnDefinitions[column].MinWidth = 50;
                    DockRoot.RowDefinitions[2].MinHeight = 50;
                    if (span)
                        Grid.SetRowSpan(ui, 5);
                    else
                        Grid.SetRow(ui, 2);
                }
                else
                {
                    Grid.SetRow(ui, row);
                    DockRoot.ColumnDefinitions[2].MinWidth = 50;
                    DockRoot.RowDefinitions[row].MinHeight = 50;
                    if (span)
                        Grid.SetColumnSpan(ui, 5);
                    else
                        Grid.SetColumn(ui, 2);
                }
            }

            if(createGridSpliter)
            {
                GridSplitter gridSplitter = new GridSplitter();
                gridSplitter.VerticalAlignment = VerticalAlignment.Stretch;
                gridSplitter.HorizontalAlignment = HorizontalAlignment.Stretch;

                if (leftright)
                {
                    gridSplitter.Width = 5;
                    Grid.SetColumn(gridSplitter, dock == Dock.Left ? column + 1 : column - 1);
                    if (span)
                        Grid.SetRowSpan(gridSplitter, 5);
                    else
                        Grid.SetRow(gridSplitter, 2);
                }
                else
                {
                    gridSplitter.Height = 5;
                    Grid.SetRow(gridSplitter, dock == Dock.Top ? row + 1 : row - 1);
                    if (span)
                        Grid.SetColumnSpan(gridSplitter, 5);
                    else
                        Grid.SetColumn(gridSplitter, 2);
                }

                DockRoot.Children.Add(gridSplitter);
            }

            if(column >= 0 && length != null)
            {
                DockRoot.ColumnDefinitions[column].Width = new GridLength(length.Value);
            }
            if (row >= 0 && length != null)
            {
                DockRoot.RowDefinitions[row].Height = new GridLength(length.Value);
            }
            DockRoot.Children.Add(ui);
        }
    }
}
