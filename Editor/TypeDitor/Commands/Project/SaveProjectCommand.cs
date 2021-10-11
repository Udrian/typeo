using TypeD.Models.Interfaces;

namespace TypeDitor.Commands.Project
{
    class SaveProjectCommand : ProjectCommands
    {
        // Models
        public ISaveModel SaveModel { get; set; }

        // Constructors
        public SaveProjectCommand(ISaveModel saveModel)
        {
            SaveModel = saveModel;
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
