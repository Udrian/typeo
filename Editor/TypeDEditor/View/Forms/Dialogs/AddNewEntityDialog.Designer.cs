
namespace TypeDEditor.View.Forms.Dialogs
{
    partial class AddNewEntityDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.tbNamespace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbUpdatable = new System.Windows.Forms.CheckBox();
            this.cbDrawable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 65);
            this.label1.TabIndex = 1;
            this.label1.Text = "New Entity";
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(635, 376);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(25);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(131, 40);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // tbNamespace
            // 
            this.tbNamespace.Location = new System.Drawing.Point(12, 237);
            this.tbNamespace.Name = "tbNamespace";
            this.tbNamespace.Size = new System.Drawing.Size(659, 35);
            this.tbNamespace.TabIndex = 9;
            this.tbNamespace.Text = "Entities";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "Namespace";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 166);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(659, 35);
            this.tbName.TabIndex = 10;
            this.tbName.Text = "Entity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Name";
            // 
            // cbUpdatable
            // 
            this.cbUpdatable.AutoSize = true;
            this.cbUpdatable.Location = new System.Drawing.Point(12, 306);
            this.cbUpdatable.Name = "cbUpdatable";
            this.cbUpdatable.Size = new System.Drawing.Size(134, 34);
            this.cbUpdatable.TabIndex = 11;
            this.cbUpdatable.Text = "Updatable";
            this.cbUpdatable.UseVisualStyleBackColor = true;
            // 
            // cbDrawable
            // 
            this.cbDrawable.AutoSize = true;
            this.cbDrawable.Location = new System.Drawing.Point(12, 346);
            this.cbDrawable.Name = "cbDrawable";
            this.cbDrawable.Size = new System.Drawing.Size(126, 34);
            this.cbDrawable.TabIndex = 12;
            this.cbDrawable.Text = "Drawable";
            this.cbDrawable.UseVisualStyleBackColor = true;
            // 
            // AddNewEntityDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbDrawable);
            this.Controls.Add(this.cbUpdatable);
            this.Controls.Add(this.tbNamespace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.label1);
            this.Name = "AddNewEntityDialog";
            this.Text = "New Entity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox tbNamespace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbUpdatable;
        private System.Windows.Forms.CheckBox cbDrawable;
    }
}