using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeDitor.Commands
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
            var project = param as Project;

            if (SaveModel.AnythingToSave)
            {
                await SaveModel.Save();
            }
            await ProjectModel.Build(project);
        }
    }
}
