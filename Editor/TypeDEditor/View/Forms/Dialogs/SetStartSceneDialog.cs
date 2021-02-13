using System;
using System.Windows.Forms;
using TypeD.Data;
using TypeDEditor.Controller;

namespace TypeDEditor.View.Forms.Dialogs
{
    public partial class SetStartSceneDialog : Form
    {
        private class CbItem
        {
            public TypeDType TypeDType { get; set; }
            public override string ToString()
            {
                return TypeDType.FullName;
            }
        }

        public TypeDType TypeDType { get; private set; }

        public SetStartSceneDialog()
        {
            InitializeComponent();

            foreach(var typeDType in ProjectController.LoadedProject.TypeDTypes.Values)
            {
                if(typeDType.TypeType == TypeDTypeType.Scene)
                {
                    cbScene.Items.Add(new CbItem() { TypeDType = typeDType });
                    if (typeDType.FullName.EndsWith(ProjectController.LoadedProject.StartScene))
                    {
                        cbScene.SelectedIndex = cbScene.Items.Count - 1;
                    }
                }
            }

            if (cbScene.Items.Count == 0)
            {
                btnSet.Enabled = false;
                btnSet.Enabled = false;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            TypeDType = (cbScene.SelectedItem as CbItem).TypeDType;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
