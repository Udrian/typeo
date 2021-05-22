using System;
using System.Windows.Forms;
using TypeD.Types;
using TypeDEditor.Controller;

namespace TypeDEditor.View.Forms.Dialogs
{
    public partial class AddEntityDialog : Form
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

        public AddEntityDialog()
        {
            InitializeComponent();

            foreach (var typeDType in ProjectController.LoadedProject.TypeOTypes.Values)
            {
                if (typeDType.TypeOBaseType == "Entity")
                {
                    cbEntity.Items.Add(new CbItem() { TypeOType = typeDType });
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
            TypeOType = (cbEntity.SelectedItem as CbItem).TypeOType;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
