using TypeDCore.Commands.Data;
using TypeD.Models.Providers.Interfaces;
using TypeD.Commands;
using System.Windows;
using TypeD.Models.Interfaces;

namespace TypeDCore.Commands
{
    public class DeleteComponentTypeCommand : CustomCommand
    {
        // Providers
        IComponentProvider ComponentProvider { get; set; }

        // Constructors
        public DeleteComponentTypeCommand(IResourceModel resourceModel) : base(resourceModel)
        {
            ComponentProvider = ResourceModel.Get<IComponentProvider>();
        }

        public override void Execute(object parameter)
        {
            var data = parameter as ComponentCommandData;
            if (data == null) return;

            var result = MessageBox.Show($"Do you want to delete '{data.Component.FullName}'?", "Deleting", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                ComponentProvider.Delete(data.Project, data.Component);
            }
        }
    }
}
