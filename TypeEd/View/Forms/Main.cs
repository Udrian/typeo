using System.Windows.Forms;
using TypeEd.Controller;
using TypeEd.Model;

namespace TypeEd.View.Forms
{
    public partial class Main : Form
    {
        public FileController FileController { get; set; }

        public Main()
        {
            InitializeComponent();

            toolStripMenuItemNew.Click += ToolStripMenuItemNew_Click;
            toolStripMenuItemOpen.Click += ToolStripMenuItemOpen_Click;
            toolStripMenuItemSave.Click += ToolStripMenuItemSave_Click;
            toolStripMenuItemExit.Click += ToolStripMenuItemExit_Click;

            FileController = new FileController();

            /*var projectPath = @"C:\Users\simon\projects\typeo";
            var project = Project.Create("Project", projectPath);
            project.ScanForSolution();
            project.ProjectDLLPath = @"Samples\SpaceInvader\bin\Debug\netcoreapp3.1\SpaceInvader.dll";
            */

        }

        private void ToolStripMenuItemNew_Click(object sender, System.EventArgs e)
        {
            var npd = new NewProjectDialog();
            npd.ShowDialog(this);
        }

        private void ToolStripMenuItemOpen_Click(object sender, System.EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                explorer.Clear();

                var project = FileController.Open(openFileDialog.FileName);

                if(project != null)
                    explorer.PopulateTree(project);
            }
        }

        private void ToolStripMenuItemSave_Click(object sender, System.EventArgs e)
        {
            FileController.Save();
        }

        private void ToolStripMenuItemExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
