using TypeD.Models.Interfaces;

namespace TypeDitor.Commands.Project
{
    class RunProjectCommand : ProjectCommands
    {
        // Models
        private IProjectModel ProjectModel { get; set; }

        public RunProjectCommand(IProjectModel projectModel)
        {
            ProjectModel = projectModel;
        }

        public override void Execute(object param)
        {
            if (param is TypeD.Models.Data.Project)
                ProjectModel.Run(param as TypeD.Models.Data.Project);
        }
    }
}
