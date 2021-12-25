using System;
using System.IO;
using System.Text;
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
        class ConsoleWriter : TextWriter
        {
            public override Encoding Encoding { get { return Encoding.UTF8; } }

            public override void Write(string value)
            {
                WriteEvent?.Invoke(this, value);
                base.Write(value);
            }

            public override void WriteLine(string value)
            {
                WriteEvent?.Invoke(this, value);
                base.WriteLine(value);
            }

            public event EventHandler<string> WriteEvent;
        }

        DrawableViewer DrawableViewer { get; set; }
        TextWriter OldTextWriter;
        ConsoleWriter NewTextWriter { get; set; }

        public ConsoleViewerDocument(Project project, Component component)
        {
            InitializeComponent();

            OldTextWriter = Console.Out;
            NewTextWriter = new ConsoleWriter();
            NewTextWriter.WriteEvent += ConsoleWriter_WriteEvent;
            Console.SetOut(NewTextWriter);

            if (component.TypeOBaseType == "Drawable2d")
            {
                DrawableViewer = new DrawableViewer(project, component);
            }
        }

        private void ConsoleWriter_WriteEvent(object sender, string e)
        {
            Dispatcher.Invoke(() =>
            {
                Output.Text += e;
            });
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            NewTextWriter.WriteEvent -= ConsoleWriter_WriteEvent;
            Console.SetOut(OldTextWriter);
            DrawableViewer.Close();
        }
    }
}
