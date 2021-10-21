using TypeD.Commands;
using TypeDCore.Commands.Project.Data;
using TypeDCore.Dialogs.Project;
using TypeDCore.Models.Interfaces;

namespace TypeDCore.Commands.Project
{
    class CreateSceneCommand : CustomCommands 
    {
        // Models
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Constructors
        public CreateSceneCommand(ITypeDCoreProjectModel typeDCoreProjectModel)
        {
            TypeDCoreProjectModel = typeDCoreProjectModel;
        }

        public override void Execute(object parameter)
        {
            var data = parameter as CreateTypeCommandData;

            var dialog = new CreateSceneDialog();
            dialog.EntityNamespace = data.Namespace;
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateScene(data.Project, dialog.EntityName, dialog.EntityNamespace);
            }
        }
    }
}
