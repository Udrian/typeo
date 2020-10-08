using System;
using System.IO;
using System.Windows.Forms;
using TypeEd.Controller;

namespace TypeEd.View.Forms
{
    public partial class NewProjectDialog : Form
    {
        public FileController FileController { get; set; }

        public NewProjectDialog()
        {
            InitializeComponent();
            FileController = new FileController();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            tbSolution.Text = tbName.Text;
            tbProject.Text = tbName.Text;
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = tbLocation.Text;
            var result = folderBrowserDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                tbLocation.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private bool IsDirectory(string filePath)
        {
            return Path.GetFileName(filePath) == Path.GetFileNameWithoutExtension(filePath);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var name = Path.GetFileNameWithoutExtension(tbName.Text);
            var location = IsDirectory(tbLocation.Text) ? tbLocation.Text : Path.GetDirectoryName(tbLocation.Text);
            var solution = Path.GetFileNameWithoutExtension(tbSolution.Text);
            var project = Path.GetFileNameWithoutExtension(tbProject.Text);

            FileController.Create(name, location, solution, project);
            Close();
        }
    }
}
