using System;
using System.IO;
using System.Windows.Forms;
using TypeDEditor.Controller;

namespace TypeDEditor.View.Forms
{
    public partial class NewProjectDialog : Form
    {

        public NewProjectDialog()
        {
            InitializeComponent();

#if DEBUG
            ///DEBUG
            if (Directory.Exists(@"C:\Users\simon\projects\typeoproj\Test"))
                Directory.Delete(@"C:\Users\simon\projects\typeoproj\Test", true);
            tbName.Text = "Test";
            tbLocation.Text = @"C:\Users\simon\projects\typeoproj";
            ///
#endif
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

        private static bool IsDirectory(string filePath)
        {
            return Path.GetFileName(filePath) == Path.GetFileNameWithoutExtension(filePath);
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            Enabled = false;

            var name = Path.GetFileNameWithoutExtension(tbName.Text);
            var location = IsDirectory(tbLocation.Text) ? tbLocation.Text : Path.GetDirectoryName(tbLocation.Text);
            var solution = @$".\{tbSolution.Text}.sln";
            var project = Path.GetFileNameWithoutExtension(tbProject.Text);

            await FileController.Create(name, location, solution, project);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
