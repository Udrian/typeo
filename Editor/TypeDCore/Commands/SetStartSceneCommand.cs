using System.Windows;
using TypeD.Commands;
using TypeD.Models.Interfaces;
using TypeDCore.Commands.Data;

namespace TypeDCore.Commands
{
    public class SetStartSceneCommand : CustomCommand
    {
        // Models
        IProjectModel ProjectModel { get; set; }

        // Constructors
        public SetStartSceneCommand(IResourceModel resourceModel) : base(resourceModel)
        {
            ProjectModel = ResourceModel.Get<IProjectModel>();
        }

        public override void Execute(object parameter)
        {
            var data = parameter as ComponentCommandData;
            if (data == null) return;

            var result = MessageBox.Show($"Do you want to set '{data.Component.FullName}' as start scene?", "Set Start Scene", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                ProjectModel.SetStartScene(data.Project, data.Component);
            }
        }
    }
}
