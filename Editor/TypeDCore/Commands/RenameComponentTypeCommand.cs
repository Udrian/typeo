using TypeD.Commands;
using TypeD.Models.Providers.Interfaces;
using TypeDCore.Commands.Data;
using TypeDCore.View.Dialogs.Project;

namespace TypeDCore.Commands
{
    class RenameComponentTypeCommand : CustomCommand
    {
        // Providers
        IComponentProvider ComponentProvider { get; set; }

        // Constructors
        public RenameComponentTypeCommand(IComponentProvider componentProvider)
        {
            ComponentProvider = componentProvider;
        }

        public override void Execute(object parameter)
        {
            var data = parameter as ComponentCommandData;
            if (data == null) return;

            var dialog = new RenameComponentTypeDialog(data.Component);
            if (dialog.ShowDialog() == true)
            {
                ComponentProvider.Rename(data.Project, data.Component, dialog.ViewModel.Name);
            }
        }
    }
}
