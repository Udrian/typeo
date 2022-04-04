using TypeD.Commands;
using TypeD.Models.Interfaces;
using TypeDCore.Commands.Data;
using TypeDCore.Models.Interfaces;
using TypeDCore.View.Dialogs.Project;

namespace TypeDCore.Commands
{
    class CreateSceneTypeCommand : CustomCommand 
    {
        // Models
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Constructors
        public CreateSceneTypeCommand(IResourceModel resourceModel) : base(resourceModel)
        {
            TypeDCoreProjectModel = ResourceModel.Get<ITypeDCoreProjectModel>();
        }

        public override void Execute(object parameter)
        {
            var data = parameter as CreateComponentCommandData;

            var dialog = new CreateSceneTypeDialog(data.Project, data.Namespace);
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateScene(data.Project, dialog.ViewModel.ComponentName, dialog.ViewModel.ComponentNamespace, dialog.ViewModel.ParentComponent);
            }
        }
    }
}
