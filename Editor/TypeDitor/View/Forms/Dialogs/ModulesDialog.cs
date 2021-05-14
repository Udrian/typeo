using System;
using System.Windows.Forms;
using TypeD.Commands.Module;
using TypeDEditor.Controller;

namespace TypeDEditor.View.Forms.Dialogs
{
    public partial class ModulesDialog : Form
    {
        public ModulesDialog()
        { 
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void ModulesDialog_Load(object sender, EventArgs e)
        {
            if (ProjectController.LoadedProject == null) return;

            var modules = await ModuleCommand.List();

            if (modules != null)
            {
                foreach (var module in modules.Modules)
                {
                    var found = false;
                    foreach (var projectModule in ProjectController.LoadedProject.Modules)
                    {
                        if(projectModule.Name == module.Key)
                        {
                            found = true;
                        }
                        if (found) continue;
                    }
                    clbModules.Items.Add($"{module.Key}", found);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var modules = await ModuleCommand.List();

            if (modules == null) return;
            
            foreach (var module in modules.Modules)
            {
                if(module.Key == clbModules.GetItemText(clbModules.SelectedItem))
                {
                    await ModuleCommand.Download(module.Key, module.Value[0]);
                    ModuleCommand.Add(module.Key, module.Value[0], ProjectController.LoadedProject);
                }
            }
            Close();
        }
    }
}
