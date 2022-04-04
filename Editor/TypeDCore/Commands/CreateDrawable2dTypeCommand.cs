using TypeD.Commands;
using TypeD.Models.Interfaces;
using TypeDCore.Commands.Data;
using TypeDCore.Models.Interfaces;
using TypeDCore.View.Dialogs.Project;

namespace TypeDCore.Commands
{
    class CreateDrawable2dTypeCommand : CustomCommand<CreateComponentCommandData>
    {
        // Models
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Constructors
        public CreateDrawable2dTypeCommand(IResourceModel resourceModel) : base(resourceModel)
        {
            TypeDCoreProjectModel = ResourceModel.Get<ITypeDCoreProjectModel>();
        }

        public override void Execute(CreateComponentCommandData parameter)
        {
            var dialog = new CreateDrawable2dTypeDialog(parameter.Project, parameter.Namespace);
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateDrawable2d(parameter.Project, dialog.ViewModel.ComponentName, dialog.ViewModel.ComponentNamespace, dialog.ViewModel.ParentComponent);
            }
        }
    }
}
