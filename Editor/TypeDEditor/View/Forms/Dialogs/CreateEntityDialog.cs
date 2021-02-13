using System;
using System.Windows.Forms;

namespace TypeDEditor.View.Forms.Dialogs
{
    public partial class CreateEntityDialog : Form
    {
        public string EntityName { get; private set; }
        public string EntityNamespace { get; private set; }
        public bool Updatable { get; private set; }
        public bool Drawable { get; private set; }

        public CreateEntityDialog()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            EntityName = tbName.Text;
            EntityNamespace = tbNamespace.Text;
            Updatable = cbUpdatable.Checked;
            Drawable = cbDrawable.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
