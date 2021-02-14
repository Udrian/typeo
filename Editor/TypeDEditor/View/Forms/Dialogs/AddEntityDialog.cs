using System;
using System.Windows.Forms;
using TypeD.Data;
using TypeDEditor.Controller;

namespace TypeDEditor.View.Forms.Dialogs
{
    public partial class AddEntityDialog : Form
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

        public AddEntityDialog()
        {
            InitializeComponent();

            foreach (var typeDType in ProjectController.LoadedProject.TypeDTypes.Values)
            {
                if (typeDType.TypeType == TypeDTypeType.Entity)
                {
                    cbEntity.Items.Add(new CbItem() { TypeDType = typeDType });
                }
            }

            if (cbEntity.Items.Count == 0)
            {
                cbEntity.Enabled = false;
                btnAdd.Enabled = false;
            }
            else
            {
                cbEntity.SelectedIndex = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TypeDType = (cbEntity.SelectedItem as CbItem).TypeDType;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
