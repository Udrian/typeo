using System.Windows.Forms;
using TypeD.Data;
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
            toolStripMenuItemCreateEntity.Click += ToolStripMenuItemCreateEntity_Click;
            toolStripMenuItemCreateScene.Click += ToolStripMenuItemCreateScene_Click;
            toolStripMenuItemSetStartScene.Click += ToolStripMenuItemSetStartScene_Click;
            toolStripMenuItemAddEntityToScene.Click += ToolStripMenuItemAddEntityToScene_Click;

            OriginalTitle = Text;

            explorer.NodeSelect += Explorer_NodeSelect;

            Hide();
        }

        private void ToolStripMenuItemAddEntityToScene_Click(object sender, System.EventArgs e)
        {
            var dialog = new AddEntityDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ProjectController.AddEntity(viewer.TabControl.SelectedTab.Tag as TypeDType, dialog.TypeDType);
            }
        }

        private void Explorer_NodeSelect(TypeD.Data.TypeDType obj)
        {
            viewer.TabControl.TabPages.Add(obj.Name);
            viewer.TabControl.TabPages[viewer.TabControl.TabCount - 1].Tag = obj;
            viewer.TabControl.SelectedIndex = viewer.TabControl.TabCount - 1;
        }

        private void ToolStripMenuItemSetStartScene_Click(object sender, System.EventArgs e)
        {
            var dialog = new SetStartSceneDialog();
            if(dialog.ShowDialog(this) == DialogResult.OK)
            {
                ProjectController.SetStartScene(dialog.TypeDType);
            }
        }

        //Add to Project
        private void ToolStripMenuItemCreateEntity_Click(object sender, System.EventArgs e)
        {
            var dialog = new CreateEntityDialog();

            if(dialog.ShowDialog(this) == DialogResult.OK)
            {
                ProjectController.CreateEntity(dialog.EntityName, dialog.EntityNamespace, dialog.Updatable, dialog.Drawable);
            }
        }

        private void ToolStripMenuItemCreateScene_Click(object sender, System.EventArgs e)
        {
            var dialog = new CreateSceneDialog();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ProjectController.CreateScene(dialog.SceneName, dialog.SceneNamespace);
            }
        }

        private async void ToolStripMenuItemRunProject_Click(object sender, System.EventArgs e)
        {
            await ProjectController.Run();
        }

        private async void ToolStripMenuItemBuildProject_Click(object sender, System.EventArgs e)
        {
            await ProjectController.Build();
            ProjectLoaded(ProjectController.LoadedProject);
        }

        private void ToolStripMenuItemNewProject_Click(object sender, System.EventArgs e)
        {
            var dialog = new NewProjectDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ProjectLoaded(ProjectController.LoadedProject);
            }
        }

        private async void ToolStripMenuItemOpen_Click(object sender, System.EventArgs e)
        {
            if(openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                await FileController.Open(openFileDialog.FileName);
                ProjectLoaded(ProjectController.LoadedProject);
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
