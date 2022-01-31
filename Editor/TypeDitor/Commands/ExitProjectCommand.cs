using System.Windows;
using TypeD.Models.Interfaces;

namespace TypeDitor.Commands
{
    class ExitProjectCommand : ProjectCommands
    {
        public ISaveModel SaveModel { get; set; }

        public ExitProjectCommand(ISaveModel saveModel)
        {
            SaveModel = saveModel;
        }

        public override void Execute(object param)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
