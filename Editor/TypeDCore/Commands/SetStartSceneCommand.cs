using System.Windows;
using TypeD.Commands;
using TypeD.Models.Interfaces;
using TypeDCore.Commands.Data;

namespace TypeDCore.Commands
{
    internal class SetStartSceneCommand : CustomCommand<ComponentCommandData>
    {
        // Models
        IProjectModel ProjectModel { get; set; }

        // Constructors
        public SetStartSceneCommand(IResourceModel resourceModel) : base(resourceModel)
        {
            ProjectModel = ResourceModel.Get<IProjectModel>();
        }

        public override void Execute(ComponentCommandData parameter)
        {
            var result = MessageBox.Show($"Do you want to set '{parameter.Component.FullName}' as start scene?", "Set Start Scene", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                ProjectModel.SetStartScene(parameter.Project, parameter.Component);
            }
        }
    }
}
