using TypeDCore.Commands.Data;
using TypeD.Models.Providers.Interfaces;
using TypeD.Commands;
using System.Windows;

namespace TypeDCore.Commands
{
    public class DeleteComponentTypeCommand : CustomCommand
    {
        // Models
        IComponentProvider ComponentProvider { get; set; }

        // Constructors
        public DeleteComponentTypeCommand(IComponentProvider componentProvider)
        {
            ComponentProvider = componentProvider;
        }

        public override void Execute(object parameter)
        {
            var data = parameter as DeleteComponentTypeCommandData;
            if (data == null) return;

            var result = MessageBox.Show($"Do you want to delete '{data.Component.FullName}'?", "Deleting", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                ComponentProvider.Delete(data.Project, data.Component);
            }
        }
    }
}
