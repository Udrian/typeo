
namespace TypeDEditor.View.Forms.Dialogs
{
    partial class SetStartSceneDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbScene = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 30);
            this.label2.TabIndex = 22;
            this.label2.Text = "Scene";
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Location = new System.Drawing.Point(635, 376);
            this.btnSet.Margin = new System.Windows.Forms.Padding(25);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(131, 40);
            this.btnSet.TabIndex = 20;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 65);
            this.label1.TabIndex = 19;
            this.label1.Text = "Set Start Scene";
            // 
            // cbScene
            // 
            this.cbScene.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScene.FormattingEnabled = true;
            this.cbScene.Location = new System.Drawing.Point(12, 166);
            this.cbScene.Name = "cbScene";
            this.cbScene.Size = new System.Drawing.Size(659, 38);
            this.cbScene.TabIndex = 25;
            this.cbScene.SelectedIndexChanged += new System.EventHandler(this.cbScene_SelectedIndexChanged);
            // 
            // SetStartSceneDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbScene);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.label1);
            this.Name = "SetStartSceneDialog";
            this.Text = "Set Start Scene";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbScene;
    }
}