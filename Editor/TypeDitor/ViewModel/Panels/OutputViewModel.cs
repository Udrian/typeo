using System;
using System.ComponentModel;
using TypeD.Helpers;
using TypeD.Models.Interfaces;

namespace TypeDitor.ViewModel.Panels
{
    class OutputViewModel: INotifyPropertyChanged
    {
        // Models
        ILogModel LogModel { get; set; }

        // Constructors
        public OutputViewModel(ILogModel logModel)
        {
            LogModel = logModel;
            LogModel.AttachLogOutput("OutputView", (message) =>
            {
                OnCMDOutput(message);
            });
            CMD.Output += OnCMDOutput;
        }

        // Functions
        private void OnCMDOutput(string output)
        {
            OutputText += output + Environment.NewLine;
        }

        public void Unload()
        {
            LogModel.DetachLogOutput("OutputView");
            CMD.Output -= OnCMDOutput;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Properties
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
