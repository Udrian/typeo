namespace TypeD.View.Forms
{
    partial class Explorer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Game");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Scenes");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Entities");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Stubs");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Logics");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Drawables");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("EntityDatas");
            this.treeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode1.Name = "Game";
            treeNode1.Text = "Game";
            treeNode2.Name = "Scenes";
            treeNode2.Text = "Scenes";
            treeNode3.Name = "Entities";
            treeNode3.Text = "Entities";
            treeNode4.Name = "Stubs";
            treeNode4.Text = "Stubs";
            treeNode5.Name = "Logics";
            treeNode5.Text = "Logics";
            treeNode6.Name = "Drawables";
            treeNode6.Text = "Drawables";
            treeNode7.Name = "EntityDatas";
            treeNode7.Text = "EntityDatas";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            this.treeView.Size = new System.Drawing.Size(240, 359);
            this.treeView.TabIndex = 0;
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Name = "Explorer";
            this.Size = new System.Drawing.Size(240, 359);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
    }
}
