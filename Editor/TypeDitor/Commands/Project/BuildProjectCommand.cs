using TypeD.Models.Interfaces;

namespace TypeDitor.Commands.Project
{
    class BuildProjectCommand : ProjectCommands
    {
        // Models
        private IProjectModel ProjectModel { get; set; }
        public ISaveModel SaveModel { get; set; }

        public BuildProjectCommand(IProjectModel projectModel, ISaveModel saveModel)
        {
            ProjectModel = projectModel;
            SaveModel = saveModel;
        }

        public async override void Execute(object param)
        {
            if (SaveModel.AnythingToSave)
            {
                await SaveModel.Save();
            }
            if(param is TypeD.Models.Data.Project)
                await ProjectModel.Build(param as TypeD.Models.Data.Project);
        }
    }
}
