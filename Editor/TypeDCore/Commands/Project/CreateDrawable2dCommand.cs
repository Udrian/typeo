﻿using TypeD.Commands;
using TypeDCore.Commands.Project.Data;
using TypeDCore.Dialogs.Project;
using TypeDCore.Models.Interfaces;

namespace TypeDCore.Commands.Project
{
    class CreateDrawable2dCommand : CustomCommands 
    {
        // Models
        ITypeDCoreProjectModel TypeDCoreProjectModel { get; set; }

        // Constructors
        public CreateDrawable2dCommand(ITypeDCoreProjectModel typeDCoreProjectModel)
        {
            TypeDCoreProjectModel = typeDCoreProjectModel;
        }

        public override void Execute(object parameter)
        {
            var data = parameter as CreateTypeCommandData;

            var dialog = new CreateDrawable2dDialog();
            dialog.EntityNamespace = data.Namespace;
            if(dialog.ShowDialog() == true)
            {
                TypeDCoreProjectModel.CreateDrawable2d(data.Project, dialog.EntityName, dialog.EntityNamespace);
            }
        }
    }
}