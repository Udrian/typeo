using System.Windows.Forms;
using TypeD.Models;
using TypeDEditor.Controller;
using TypeDEditor.View.Forms.Dialogs;

namespace TypeDEditor.View.Forms
{
    public partial class Main : Form
    {
        private readonly string OriginalTitle;

        public Main()
        {
            InitializeComponent();

            toolStripMenuItemNewProject.Click += ToolStripMenuItemNewProject_Click;
            toolStripMenuItemOpen.Click += ToolStripMenuItemOpen_Click;
            toolStripMenuItemSave.Click += ToolStripMenuItemSave_Click;
            toolStripMenuItemExit.Click += ToolStripMenuItemExit_Click;
            toolStripMenuItemBuildProject.Click += ToolStripMenuItemBuildProject_Click;
            toolStripMenuItemRunProject.Click += ToolStripMenuItemRunProject_Click;
            toolStripMenuItemAddEntity.Click += ToolStripMenuItemAddEntity_Click;
            toolStripMenuItemAddScene.Click += ToolStripMenuItemAddScene_Click;

            OriginalTitle = Text;

            Hide();
        }

        //Add to Project
        private void ToolStripMenuItemAddEntity_Click(object sender, System.EventArgs e)
        {
            var dialog = new AddNewEntityDialog();
            var result = dialog.ShowDialog(this);

            if(result == DialogResult.OK)
            {
                ProjectController.AddNewEntity(dialog.EntityName, dialog.EntityNamespace, dialog.Updatable, dialog.Drawable);
            }
        }

        private void ToolStripMenuItemAddScene_Click(object sender, System.EventArgs e)
        {
            var dialog = new AddNewSceneDialog();
            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                ProjectController.AddNewScene(dialog.SceneName, dialog.SceneNamespace);
            }
        }

        private async void ToolStripMenuItemRunProject_Click(object sender, System.EventArgs e)
        {
            await ProjectController.Run();
        }

        private async void ToolStripMenuItemBuildProject_Click(object sender, System.EventArgs e)
        {
            await ProjectController.Build();
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
                await FileController.Open(openFileDialog.FileName);
                ProjectLoaded(FileController.LoadedProject);
            }
        }

        public void ProjectLoaded(ProjectModel project)
        {
            if (project == null) return;

            Text = $"{OriginalTitle} - {project.ProjectName} - {project.ProjectFilePath}";
        }

        private async void ToolStripMenuItemSave_Click(object sender, System.EventArgs e)
        {
            await FileController.Save();
        }

        private void ToolStripMenuItemExit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void Main_Load(object sender, System.EventArgs e)
        {
            new Splash(this).ShowDialog();
        }

        public OpenFileDialog GetOpenFileDialog()
        {
            return openFileDialog;
        }
    }
}
