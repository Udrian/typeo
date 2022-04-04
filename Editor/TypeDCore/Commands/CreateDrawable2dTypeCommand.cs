using TypeD.Commands;
using TypeD.Models.Interfaces;
using TypeDCore.Commands.Data;
using TypeDCore.Models.Interfaces;
using TypeDCore.View.Dialogs.Project;

namespace TypeDCore.Commands
{
    class CreateDrawable2dTypeCommand : CustomCommand 
    {
        // Models
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Constructors
        public CreateDrawable2dTypeCommand(IResourceModel resourceModel) : base(resourceModel)
        {
            TypeDCoreProjectModel = ResourceModel.Get<ITypeDCoreProjectModel>();
        }

        public override void Execute(object parameter)
        {
            var data = parameter as CreateComponentCommandData;

            var dialog = new CreateDrawable2dTypeDialog(data.Project, data.Namespace);
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateDrawable2d(data.Project, dialog.ViewModel.ComponentName, dialog.ViewModel.ComponentNamespace, dialog.ViewModel.ParentComponent);
            }
        }
    }
}
