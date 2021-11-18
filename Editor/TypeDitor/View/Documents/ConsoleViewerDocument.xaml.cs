using System.Windows.Controls;
using TypeD.Models.Data;
using TypeD.Viewer;

namespace TypeDitor.View.Documents
{
    /// <summary>
    /// Interaction logic for ConsoleViewerDocument.xaml
    /// </summary>
    public partial class ConsoleViewerDocument : UserControl
    {
        DrawableViewer DrawableViewer { get; set; }

        public ConsoleViewerDocument(Project project, TypeOType typeOType)
        {
            InitializeComponent();

            DrawableViewer = new DrawableViewer(project, typeOType);

            DrawableViewer.ConsoleWriter.WriteEvent += ConsoleWriter_WriteEvent;
        }

        private void ConsoleWriter_WriteEvent(object sender, string e)
        {
            Dispatcher.Invoke(() =>
            {
                Console.Text += e;
            });
        }
    }
}
