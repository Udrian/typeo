namespace TypeDEditor.View.Forms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCreateEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCreateScene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCreateDrawable2d = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSetStartScene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAddEntityToScene = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBuildProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRunProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.explorer = new TypeDEditor.View.Forms.Explorer();
            this.output = new TypeDEditor.View.Forms.Output();
            this.viewer = new TypeDEditor.View.Forms.Viewer();
            this.toolStripMenuItemAddDrawable2dToEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemProject,
            this.toolStripMenuItemBuild,
            this.toolStripMenuItemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1692, 38);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNew,
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemSave,
            this.toolStripMenuItemExit});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(62, 34);
            this.toolStripMenuItemFile.Text = "File";
            // 
            // toolStripMenuItemNew
            // 
            this.toolStripMenuItemNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNewProject});
            this.toolStripMenuItemNew.Name = "toolStripMenuItemNew";
            this.toolStripMenuItemNew.Size = new System.Drawing.Size(182, 40);
            this.toolStripMenuItemNew.Text = "New";
            // 
            // toolStripMenuItemNewProject
            // 
            this.toolStripMenuItemNewProject.Name = "toolStripMenuItemNewProject";
            this.toolStripMenuItemNewProject.Size = new System.Drawing.Size(195, 40);
            this.toolStripMenuItemNewProject.Text = "Project";
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(182, 40);
            this.toolStripMenuItemOpen.Text = "Open";
            // 
            // toolStripMenuItemSave
            // 
            this.toolStripMenuItemSave.Name = "toolStripMenuItemSave";
            this.toolStripMenuItemSave.Size = new System.Drawing.Size(182, 40);
            this.toolStripMenuItemSave.Text = "Save";
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(182, 40);
            this.toolStripMenuItemExit.Text = "Exit";
            // 
            // toolStripMenuItemProject
            // 
            this.toolStripMenuItemProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCreate,
            this.toolStripMenuItemSetStartScene,
            this.toolStripMenuItemAddEntityToScene,
            this.toolStripMenuItemAddDrawable2dToEntity});
            this.toolStripMenuItemProject.Name = "toolStripMenuItemProject";
            this.toolStripMenuItemProject.Size = new System.Drawing.Size(95, 34);
            this.toolStripMenuItemProject.Text = "Project";
            // 
            // toolStripMenuItemCreate
            // 
            this.toolStripMenuItemCreate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCreateEntity,
            this.toolStripMenuItemCreateScene,
            this.toolStripMenuItemCreateDrawable2d});
            this.toolStripMenuItemCreate.Name = "toolStripMenuItemCreate";
            this.toolStripMenuItemCreate.Size = new System.Drawing.Size(368, 40);
            this.toolStripMenuItemCreate.Text = "Create";
            // 
            // toolStripMenuItemCreateEntity
            // 
            this.toolStripMenuItemCreateEntity.Name = "toolStripMenuItemCreateEntity";
            this.toolStripMenuItemCreateEntity.Size = new System.Drawing.Size(241, 40);
            this.toolStripMenuItemCreateEntity.Text = "Entity";
            // 
            // toolStripMenuItemCreateScene
            // 
            this.toolStripMenuItemCreateScene.Name = "toolStripMenuItemCreateScene";
            this.toolStripMenuItemCreateScene.Size = new System.Drawing.Size(241, 40);
            this.toolStripMenuItemCreateScene.Text = "Scene";
            // 
            // toolStripMenuItemCreateDrawable2d
            // 
            this.toolStripMenuItemCreateDrawable2d.Name = "toolStripMenuItemCreateDrawable2d";
            this.toolStripMenuItemCreateDrawable2d.Size = new System.Drawing.Size(241, 40);
            this.toolStripMenuItemCreateDrawable2d.Text = "Drawable2d";
            // 
            // toolStripMenuItemSetStartScene
            // 
            this.toolStripMenuItemSetStartScene.Name = "toolStripMenuItemSetStartScene";
            this.toolStripMenuItemSetStartScene.Size = new System.Drawing.Size(368, 40);
            this.toolStripMenuItemSetStartScene.Text = "Set start scene";
            // 
            // toolStripMenuItemAddEntityToScene
            // 
            this.toolStripMenuItemAddEntityToScene.Name = "toolStripMenuItemAddEntityToScene";
            this.toolStripMenuItemAddEntityToScene.Size = new System.Drawing.Size(368, 40);
            this.toolStripMenuItemAddEntityToScene.Text = "Add Entity To Scene";
            // 
            // toolStripMenuItemBuild
            // 
            this.toolStripMenuItemBuild.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemBuildProject,
            this.toolStripMenuItemRunProject});
            this.toolStripMenuItemBuild.Name = "toolStripMenuItemBuild";
            this.toolStripMenuItemBuild.Size = new System.Drawing.Size(77, 34);
            this.toolStripMenuItemBuild.Text = "Build";
            // 
            // toolStripMenuItemBuildProject
            // 
            this.toolStripMenuItemBuildProject.Name = "toolStripMenuItemBuildProject";
            this.toolStripMenuItemBuildProject.Size = new System.Drawing.Size(247, 40);
            this.toolStripMenuItemBuildProject.Text = "Build Project";
            // 
            // toolStripMenuItemRunProject
            // 
            this.toolStripMenuItemRunProject.Name = "toolStripMenuItemRunProject";
            this.toolStripMenuItemRunProject.Size = new System.Drawing.Size(247, 40);
            this.toolStripMenuItemRunProject.Text = "Run Project";
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(74, 34);
            this.toolStripMenuItemHelp.Text = "Help";
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(188, 40);
            this.toolStripMenuItemAbout.Text = "About";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip.Location = new System.Drawing.Point(0, 1071);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1692, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "TypeD Project file|*.typeo";
            // 
            // explorer
            // 
            this.explorer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.explorer.Location = new System.Drawing.Point(12, 41);
            this.explorer.Name = "explorer";
            this.explorer.Size = new System.Drawing.Size(363, 1027);
            this.explorer.TabIndex = 2;
            // 
            // output
            // 
            this.output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.output.Location = new System.Drawing.Point(381, 731);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(1299, 337);
            this.output.TabIndex = 3;
            // 
            // viewer
            // 
            this.viewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewer.Location = new System.Drawing.Point(381, 41);
            this.viewer.Name = "viewer";
            this.viewer.Size = new System.Drawing.Size(1299, 684);
            this.viewer.TabIndex = 4;
            // 
            // toolStripMenuItemAddDrawable2dToEntity
            // 
            this.toolStripMenuItemAddDrawable2dToEntity.Name = "toolStripMenuItemAddDrawable2dToEntity";
            this.toolStripMenuItemAddDrawable2dToEntity.Size = new System.Drawing.Size(368, 40);
            this.toolStripMenuItemAddDrawable2dToEntity.Text = "Add Drawable2d to Entity";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1692, 1093);
            this.Controls.Add(this.viewer);
            this.Controls.Add(this.output);
            this.Controls.Add(this.explorer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "Main";
            this.Text = "TypeD - A TypeO Editor";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNew;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
        private Explorer explorer;
        private TypeDEditor.View.Forms.Output output;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNewProject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBuild;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBuildProject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRunProject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemProject;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCreate;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCreateEntity;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCreateScene;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSetStartScene;
        private Viewer viewer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddEntityToScene;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCreateDrawable2d;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddDrawable2dToEntity;
    }
}

