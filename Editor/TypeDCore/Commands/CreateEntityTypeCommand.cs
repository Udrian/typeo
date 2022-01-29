﻿using TypeD.Commands;
using TypeDCore.Commands.Data;
using TypeDCore.Models.Interfaces;
using TypeDCore.View.Dialogs.Project;

namespace TypeDCore.Commands
{
    class CreateEntityTypeCommand : CustomCommand 
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
            var data = parameter as CreateComponentCommandData;

            var dialog = new CreateEntityTypeDialog(data.Project);
            dialog.ViewModel.ComponentNamespace = data.Namespace;
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateEntity(data.Project, dialog.ViewModel.ComponentName, dialog.ViewModel.ComponentNamespace, dialog.ViewModel.ComponentUpdatable, dialog.ViewModel.ComponentDrawable);
            }
        }
    }
}
