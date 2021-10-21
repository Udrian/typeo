using TypeD.Commands;
using TypeDCore.Commands.Project.Data;
using TypeDCore.Dialogs.Project;
using TypeDCore.Models.Interfaces;

namespace TypeDCore.Commands.Project
{
    class CreateEntityTypeCommand : CustomCommands 
    {
        // Models
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Constructors
        public CreateEntityTypeCommand(ITypeDCoreProjectModel typeDCoreProjectModel)
        {
            TypeDCoreProjectModel = typeDCoreProjectModel;
        }

        public override void Execute(object parameter)
        {
            var data = parameter as CreateTypeCommandData;

            var dialog = new CreateEntityTypeDialog();
            dialog.EntityNamespace = data.Namespace;
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateEntity(data.Project, dialog.EntityName, dialog.EntityNamespace, dialog.EntityUpdatable, dialog.EntityDrawable);
            }
        }
    }
}
