using System;
using System.IO;
using System.Windows.Forms;
using TypeOEditor.Controller;
using TypeOEditor.Model;

namespace TypeOEditor.View.Forms
{
    public partial class NewProjectDialog : Form
    {
        public Project CreatedProject { get; set; }
        public FileController FileController { get; set; }

        public NewProjectDialog()
        {
            InitializeComponent();
            FileController = new FileController();
        }

        private void UpdateCreatedFields()
        {
            tbSolution.Text = CreatedProject.SolutionFilePath;
            UpdateFields();
        }
        private void UpdateFields()
        {
            var projects = CreatedProject.FetchProjectsFromSolution();

            cbProject.SelectedIndex = -1;
            cbProject.Items.Clear();

            foreach(var project in projects)
            {
                cbProject.Items.Add(project);
            }

            if(cbProject.Items.Count > 0)
            {
                cbProject.SelectedIndex = 0;
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            CreatedProject = FileController.Create(tbName.Text, tbLocation.Text);
            UpdateCreatedFields();
        }

        private void tbLocation_TextChanged(object sender, EventArgs e)
        {
            CreatedProject = FileController.Create(tbName.Text, tbLocation.Text);
            UpdateCreatedFields();
        }

        private void tbSolution_TextChanged(object sender, EventArgs e)
        {
            if(CreatedProject == null)
            {
                CreatedProject = FileController.Create("", "");
            }
            CreatedProject.SolutionFilePath = tbSolution.Text;
            UpdateFields();
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

        private void btnOpenSolution_Click(object sender, EventArgs e)
        {
            saveFileDialog.DefaultExt = ".sln";
            saveFileDialog.FileName = tbSolution.Text;
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbSolution.Text = saveFileDialog.FileName;
            }
        }

        private void cbProject_TextChanged(object sender, EventArgs e)
        {
            if (CreatedProject == null)
            {
                CreatedProject = FileController.Create("", "");
            }
            CreatedProject.ProjectName = cbProject.Text;
            UpdateFields();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            FileController.Create(CreatedProject);
        }
    }
}
