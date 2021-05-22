using System;
using System.Windows.Forms;
using TypeD.Models;
using TypeD.Types;
using TypeDCore.Viewer;
using TypeDEditor.Controller;
using TypeDEditor.View.Forms.Dialogs;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeDEditor.View.Forms
{
    public partial class Main : Form
    {
        private readonly string OriginalTitle;

        public Main()
        {
            //TODO: Should not be here, should be moved over to TypeDCore
            TypeOType.TypeOTypeTypes.Add(typeof(Scene));
            TypeOType.TypeOTypeTypes.Add(typeof(Entity));
            TypeOType.TypeOTypeTypes.Add(typeof(Stub));
            TypeOType.TypeOTypeTypes.Add(typeof(Logic));
            TypeOType.TypeOTypeTypes.Add(typeof(Drawable));
            TypeOType.TypeOTypeTypes.Add(typeof(EntityData));
            //


            InitializeComponent();

            toolStripMenuItemNewProject.Click += ToolStripMenuItemNewProject_Click;
            toolStripMenuItemOpen.Click += ToolStripMenuItemOpen_Click;
            toolStripMenuItemSave.Click += ToolStripMenuItemSave_Click;
            toolStripMenuItemExit.Click += ToolStripMenuItemExit_Click;
            toolStripMenuItemBuildProject.Click += ToolStripMenuItemBuildProject_Click;
            toolStripMenuItemRunProject.Click += ToolStripMenuItemRunProject_Click;
            toolStripMenuItemCreateEntity.Click += ToolStripMenuItemCreateEntity_Click;
            toolStripMenuItemCreateScene.Click += ToolStripMenuItemCreateScene_Click;
            toolStripMenuItemCreateDrawable2d.Click += ToolStripMenuItemCreateDrawable2d_Click;
            toolStripMenuItemSetStartScene.Click += ToolStripMenuItemSetStartScene_Click;
            toolStripMenuItemAddEntityToScene.Click += ToolStripMenuItemAddEntityToScene_Click;
            toolStripMenuItemAddDrawable2dToEntity.Click += ToolStripMenuItemAddDrawable2dToEntity_Click;
            toolStripMenuItemModules.Click += ToolStripMenuItemModules_Click;

            OriginalTitle = Text;

            explorer.NodeSelect += Explorer_NodeSelect;

            Hide();
        }

        private void ToolStripMenuItemModules_Click(object sender, EventArgs e)
        {
            var dialog = new ModulesDialog();
            dialog.Show();
        }

        private void ToolStripMenuItemAddDrawable2dToEntity_Click(object sender, System.EventArgs e)
        {
            var dialog = new AddDrawable2dDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK && viewer.TabControl.SelectedTab != null)
            {
                ProjectController.AddDrawable2d(viewer.TabControl.SelectedTab.Tag as TypeOType, dialog.TypeOType);
            }
        }

        private void ToolStripMenuItemAddEntityToScene_Click(object sender, System.EventArgs e)
        {
            var dialog = new AddEntityDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK && viewer.TabControl.SelectedTab != null)
            {
                ProjectController.AddEntity(viewer.TabControl.SelectedTab.Tag as TypeOType, dialog.TypeOType);
            }
        }

        private void Explorer_NodeSelect(TypeOType obj)
        {
            viewer.TabControl.TabPages.Add(obj.ClassName);
            var tab = viewer.TabControl.TabPages[viewer.TabControl.TabCount - 1];
            tab.Tag = obj;
            if(obj.TypeOBaseType == "Drawable")
            {
                var defaultStream = Console.OpenStandardInput();
                int i = 0;
                var cw = new ConsoleWriter();
                cw.WriteLineEvent += (object sender, ConsoleWriterEventArgs e) =>
                {
                    Helper.ThreadHelper.InvokeMainThread(this, () =>
                    {
                        if(tab.Controls.Count >= 25)
                        {
                            tab.Controls.Clear();
                            i = 0;
                        }

                        var label = new Label();
                        label.Text = e.Value;
                        label.Location = new System.Drawing.Point(0, i * 32);
                        i++;
                        tab.Controls.Add(label);
                    });
                };
                Console.SetOut(cw);

                var drawableViewer = new DrawableViewer(obj);
            }
            viewer.TabControl.SelectedIndex = viewer.TabControl.TabCount - 1;
        }

        private void ToolStripMenuItemSetStartScene_Click(object sender, System.EventArgs e)
        {
            var dialog = new SetStartSceneDialog();
            if(dialog.ShowDialog(this) == DialogResult.OK)
            {
                ProjectController.SetStartScene(dialog.TypeOType);
            }
        }

        //Create Project
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

        private void ToolStripMenuItemCreateDrawable2d_Click(object sender, System.EventArgs e)
        {
            var dialog = new CreateDrawable2dDialog();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ProjectController.CreateDrawable2d(dialog.SceneName, dialog.SceneNamespace);
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
