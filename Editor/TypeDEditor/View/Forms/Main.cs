using System.Windows.Forms;
using TypeD.Controller;
using TypeD.Model;

namespace TypeD.View.Forms
{
    public partial class Main : Form
    {
        private readonly string OriginalTitle;
        public FileController FileController { get; set; }

        public Main()
        {
            InitializeComponent();

            toolStripMenuItemNewProject.Click += ToolStripMenuItemNewProject_Click;
            toolStripMenuItemOpen.Click += ToolStripMenuItemOpen_Click;
            toolStripMenuItemSave.Click += ToolStripMenuItemSave_Click;
            toolStripMenuItemExit.Click += ToolStripMenuItemExit_Click;
            toolStripMenuItemBuildProject.Click += ToolStripMenuItemBuildProject_Click;
            toolStripMenuItemRunProject.Click += ToolStripMenuItemRunProject_Click;

            FileController = new FileController();

            OriginalTitle = Text;
        }

        private void ToolStripMenuItemRunProject_Click(object sender, System.EventArgs e)
        {
            FileController.LoadedProject.Run();
        }

        private async void ToolStripMenuItemBuildProject_Click(object sender, System.EventArgs e)
        {
            await FileController.LoadedProject.Build();
            FileController.LoadedProject.LoadAssembly();
            ProjectLoaded(FileController.LoadedProject);
        }

        private void ToolStripMenuItemNewProject_Click(object sender, System.EventArgs e)
        {
            var npd = new NewProjectDialog();
            var result = npd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                ProjectLoaded(FileController.LoadedProject);
            }
        }

        private async void ToolStripMenuItemOpen_Click(object sender, System.EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                var project = await FileController.Open(openFileDialog.FileName);
                ProjectLoaded(project);
            }
        }

        private void ProjectLoaded(Project project)
        {
            explorer.Clear();

            if (project == null) return;

            explorer.PopulateTree(project);
            Text = $"{OriginalTitle} - {project.Name} - {project.ProjectFilePath}";
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
