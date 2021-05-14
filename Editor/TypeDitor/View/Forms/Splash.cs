using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TypeD.Helpers;
using TypeDEditor.Controller;
using TypeDEditor.Model;
using TypeDEditor.View.Forms.Dialogs;

namespace TypeDEditor.View.Forms
{
    public partial class Splash : Form
    {
        private Main Main { get; set; }

        public Splash(Main main)
        {
            Main = main;
            InitializeComponent();

            var recents = JSON.Deserialize<List<RecentModel>>(RecentModel.RecentFilePath) ?? new List<RecentModel>();
            recents.RemoveAll((recent) => { return !File.Exists(recent.Path); });

            int y = 0, i = 0;
            foreach(var recent in recents)
            {
                if (i >= RecentModel.RecentLength) break;
                i++;

                var recentLink = new LinkLabel();
                recentLink.Text = recent.Name;
                recentLink.Links.Add(0, recentLink.Text.Length, recent.Path);
                recentLink.LinkClicked += RecentLink_LinkClicked;
                recentLink.Height = 32;
                recentLink.Width = recentPanel.Width - recentLink.Margin.Left;

                recentLink.Location = new System.Drawing.Point(recentLink.Margin.Left, recentLink.Margin.Top + y);
                recentPanel.Controls.Add(recentLink);
                y += recentLink.Size.Height;
            }
        }

        private async void RecentLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Enabled = false;
            var path = e.Link.LinkData as string;

            await FileController.Open(path);
            Main.ProjectLoaded(ProjectController.LoadedProject);
            Main.Show();
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            Main.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var npd = new NewProjectDialog();
            var result = npd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                Main.ProjectLoaded(ProjectController.LoadedProject);
                Main.Show();
                Close();
            }
        }

        private async void btnOpen_Click(object sender, EventArgs e)
        {
            var openFileDialog = Main.GetOpenFileDialog();

            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                await FileController.Open(openFileDialog.FileName);
                Main.ProjectLoaded(ProjectController.LoadedProject);
                Main.Show();
                Close();
            }
        }
    }
}
