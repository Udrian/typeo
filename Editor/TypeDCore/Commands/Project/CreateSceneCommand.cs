using TypeDCore.Dialogs.Project;
using TypeDCore.Models.Interfaces;

namespace TypeDCore.Commands.Project
{
    class CreateSceneCommand : CustomCommands 
    {
        // Models
        public ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Constructors
        public CreateSceneCommand(ITypeDCoreProjectModel typeDCoreProjectModel)
        {
            TypeDCoreProjectModel = typeDCoreProjectModel;
        }

        public override void Execute(object parameter)
        {
            var project = parameter as TypeD.Models.Data.Project;

            var dialog = new CreateSceneDialog();
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateScene(project, dialog.EntityName, dialog.EntityNamespace);
            }
        }
    }
}
