using System.Windows;
using TypeD.Models.Interfaces;

namespace TypeDitor.Commands
{
    class SaveProjectCommand : ProjectCommands
    {
        // Models
        public ISaveModel SaveModel { get; set; }

        // Constructors
        public SaveProjectCommand(FrameworkElement element) : base(element)
        {
            SaveModel = ResourceModel.Get<ISaveModel>();
        }

        public override async void Execute(object param)
        {
            await SaveModel.Save();
        }

        public override bool CanExecute(object parameter)
        {
            return SaveModel.AnythingToSave;
        }
    }
}
