using System.Windows;

namespace TypeD.View
{
    public class Panel
    {
        // Properties
        public string Title { get; set; }
        public UIElement PanelView { get; set; }

        // Constructors
        public Panel(string title, UIElement view)
        { 
            Title = title;
            PanelView = view;
        }
    }
}
