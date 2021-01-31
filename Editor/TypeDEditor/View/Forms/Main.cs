using System.Collections.Generic;
using System.Windows.Forms;
using TypeD.Models;
using TypeD.Helpers;
using TypeDEditor.Controller;
using TypeDEditor.Model;
using System.IO;

namespace TypeDEditor.View.Forms
{
    public partial class Main : Form
    {
        private readonly string OriginalTitle;
        public FileController FileController { get; set; }
        public static string RecentFilePath { get; set; } = "recent";
        public static int RecentLength { get; set; } = 5;

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

            Hide();
        }

        private void ToolStripMenuItemRunProject_Click(object sender, System.EventArgs e)
        {
            FileController.LoadedProject.Run();
        }

        private async void ToolStripMenuItemBuildProject_Click(object sender, System.EventArgs e)
        {
            await FileController.LoadedProject.Build();
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

            var recents = JSON.Deserialize<List<RecentModel>>(RecentFilePath) ?? new List<RecentModel>();

            recents.RemoveAll((recent) => { return recent.Path == project.ProjectFilePath || !File.Exists(recent.Path); });
            recents.Insert(0, new RecentModel() { Name = project.ProjectName, Path = project.ProjectFilePath });
            
            if(recents.Count > RecentLength)
            {
                recents.RemoveRange(RecentLength, recents.Count - RecentLength);
            }
            JSON.Serialize(recents, RecentFilePath);
        }

        private void ToolStripMenuItemSave_Click(object sender, System.EventArgs e)
        {
            FileController.Save();
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
