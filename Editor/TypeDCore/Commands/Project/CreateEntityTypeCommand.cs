using TypeDCore.Dialogs.Project;
using TypeDCore.Models.Interfaces;

namespace TypeDCore.Commands.Project
{
    class CreateEntityTypeCommand : CustomCommands 
    {
        // Models
        public ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Constructors
        public CreateEntityTypeCommand(ITypeDCoreProjectModel typeDCoreProjectModel)
        {
            TypeDCoreProjectModel = typeDCoreProjectModel;
        }

        public override void Execute(object parameter)
        {
            var project = parameter as TypeD.Models.Data.Project;

            var dialog = new CreateEntityTypeDialog();
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateEntity(project, dialog.EntityName, dialog.EntityNamespace, dialog.EntityUpdatable, dialog.EntityDrawable);
            }
        }
    }
}
