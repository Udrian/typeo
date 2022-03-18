using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TypeDitor.TypeDock
{
    /// <summary>
    /// Interaction logic for TypeDockRoot.xaml
    /// </summary>
    public partial class TypeDockRoot : UserControl, INotifyPropertyChanged
    {
        // Properties
        private string panelTitel;
        public string PanelTitel {
            get => panelTitel;
            set
            {
                panelTitel = value;
                NotifyPropertyChanged("PanelTitel");
            }
        }

        // Constructors
        public TypeDockRoot()
        {
            DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Functions
        public void AddPanel(UIElement ui, string panelTitel)
        {
            Grid.SetColumn(ui, 0);
            Grid.SetRow(ui, 1);

            InnerGrid.Children.Add(ui);
            PanelTitel = panelTitel;
        }

        public void AddPanel(UIElement ui, string panelTitel, Dock dock, int? length = null, bool span = false)
        {
            var newTypeDockRoot = new TypeDockRoot();
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

            if(leftright)
            {
                Grid.SetColumn(newTypeDockRoot, column);
                DockRoot.ColumnDefinitions[column].MinWidth = 50;
                DockRoot.RowDefinitions[2].MinHeight = 50;
                if (span)
                    Grid.SetRowSpan(newTypeDockRoot, 5);
                else
                    Grid.SetRow(newTypeDockRoot, 2);
            }
            else
            {
                Grid.SetRow(newTypeDockRoot, row);
                DockRoot.ColumnDefinitions[2].MinWidth = 50;
                DockRoot.RowDefinitions[row].MinHeight = 50;
                if (span)
                    Grid.SetColumnSpan(newTypeDockRoot, 5);
                else
                    Grid.SetColumn(newTypeDockRoot, 2);
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

            newTypeDockRoot.AddPanel(ui, panelTitel);
            DockRoot.Children.Add(newTypeDockRoot);
        }
    }
}
