using System;
using System.Windows.Forms;

namespace TypeDEditor.View.Forms.Dialogs
{
    public partial class AddNewSceneDialog : Form
    {
        public string SceneName { get; private set; }
        public string SceneNamespace { get; private set; }

        public AddNewSceneDialog()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            SceneName = tbName.Text;
            SceneNamespace = tbNamespace.Text;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
