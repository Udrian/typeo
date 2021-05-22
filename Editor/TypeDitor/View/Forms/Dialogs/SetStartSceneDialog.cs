using System;
using System.Windows.Forms;
using TypeD.Types;
using TypeDEditor.Controller;

namespace TypeDEditor.View.Forms.Dialogs
{
    public partial class SetStartSceneDialog : Form
    {
        private class CbItem
        {
            public TypeOType TypeOType { get; set; }
            public override string ToString()
            {
                return TypeOType.FullName;
            }
        }

        public TypeOType TypeOType { get; private set; }

        public SetStartSceneDialog()
        {
            InitializeComponent();

            foreach(var typeOType in ProjectController.LoadedProject.TypeOTypes.Values)
            {
                if(typeOType.TypeOBaseType == "Scene")
                {
                    cbScene.Items.Add(new CbItem() { TypeOType = typeOType });
                    if (ProjectController.LoadedProject.StartScene != null && typeOType.FullName.EndsWith(ProjectController.LoadedProject.StartScene))
                    {
                        cbScene.SelectedIndex = cbScene.Items.Count - 1;
                    }
                }
            }

            if(cbScene.Items.Count == 0)
            {
                cbScene.Enabled = false;
            }

            if (cbScene.SelectedIndex < 0)
            {
                btnSet.Enabled = false;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            TypeOType = (cbScene.SelectedItem as CbItem).TypeOType;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cbScene_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSet.Enabled = cbScene.SelectedIndex >= 0;
        }
    }
}
