using TypeD.Models.Interfaces;

namespace TypeDitor.Commands.Project
{
    class BuildProjectCommand : ProjectCommands
    {
        // Models
        private IProjectModel ProjectModel { get; set; }

        public BuildProjectCommand(IProjectModel projectModel)
        {
            ProjectModel = projectModel;
        }

        public async override void Execute(object param)
        {
            if(param is TypeD.Models.Data.Project)
                await ProjectModel.Build(param as TypeD.Models.Data.Project);
        }
    }
}
