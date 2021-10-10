using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TypeD.Helpers;

namespace TypeDitor.View.Panels
{
    /// <summary>
    /// Interaction logic for OutputPanel.xaml
    /// </summary>
    public partial class OutputPanel : UserControl, INotifyPropertyChanged
    {
        public OutputPanel()
        {
            this.DataContext = this;
            InitializeComponent();

            CMD.Output += OnCMDOutput;
        }

        private void OnCMDOutput(string output)
        {
            OutputText += output + Environment.NewLine;
        }

        private void tbOutputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbOutputText.ScrollToEnd();
        }

        private void Panel_Unloaded(object sender, RoutedEventArgs e)
        {
            CMD.Output -= OnCMDOutput;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private string _outputText;
        public string OutputText
        {
            get => _outputText;
            set
            {
                _outputText = value;
                NotifyPropertyChanged("OutputText");
            }
        }
    }
}
