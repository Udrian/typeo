using TypeDCore.Dialogs.Project;
using TypeDCore.Models.Interfaces;

namespace TypeDCore.Commands.Project
{
    class CreateDrawable2dCommand : CustomCommands 
    {
        // Models
        public ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Constructors
        public CreateDrawable2dCommand(ITypeDCoreProjectModel typeDCoreProjectModel)
        {
            TypeDCoreProjectModel = typeDCoreProjectModel;
        }

        public override void Execute(object parameter)
        {
            var project = parameter as TypeD.Models.Data.Project;

            var dialog = new CreateDrawable2dDialog();
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateDrawable2d(project, dialog.EntityName, dialog.EntityNamespace);
            }
        }
    }
}
