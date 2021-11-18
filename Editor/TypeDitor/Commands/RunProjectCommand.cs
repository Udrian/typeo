using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeDitor.Commands
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
            if (param is Project)
                ProjectModel.Run(param as Project);
        }
    }
}
