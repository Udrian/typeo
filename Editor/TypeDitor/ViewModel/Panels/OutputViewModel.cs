using System;
using TypeD.Helpers;
using TypeD.Models.Interfaces;
using TypeD.ViewModel;

namespace TypeDitor.ViewModel.Panels
{
    class OutputViewModel : ViewModelBase
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

        // Properties
        private string _outputText;
        public string OutputText
        {
            get => _outputText;
            set
            {
                _outputText = value;
                OnPropertyChanged(OutputText);
            }
        }
    }
}
