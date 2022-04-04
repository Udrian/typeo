using Microsoft.Win32;
using System;
using System.Windows;
using TypeD.Models.Data;
using TypeD.Models.Providers.Interfaces;
using TypeDitor.View.Dialogs.Project;

namespace TypeDitor.Commands
{
    class OpenProjectCommand : ProjectCommands
    {
        // Providers
        private IRecentProvider RecentProvider { get; set; }
        private IProjectProvider ProjectProvider { get; set; }

        public OpenProjectCommand(FrameworkElement element) : base(element)
        {
            RecentProvider = ResourceModel.Get<IRecentProvider>();
            ProjectProvider = ResourceModel.Get<IProjectProvider>();
        }

        public override void Execute(object param)
        {
            var path = "";
            if (param is Recent)
            {
                path = (param as Recent).Path;
            }
            else
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".typeo";
                openFileDialog.Filter = "TypeO Projects (*.typeo)|*.typo";
                if (openFileDialog.ShowDialog() == true)
                {
                    path = openFileDialog.FileName;
                }
            }

            //Open project
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    var progressDialog = new OpenProjectProgressDialog();
                    var openProjectTask = ProjectProvider.Load(path, (progress) => {
                        progressDialog.Progress = progress;
                        if (progress >= 100)
                        {
                            progressDialog.Close();
                        }
                    });
                    progressDialog.ShowDialog();

                    var loadedProject = openProjectTask.Result;
                    if (loadedProject != null)
                    {
                        RecentProvider.Add(loadedProject.ProjectFilePath, loadedProject.ProjectName);

                        OpenMainWindow(loadedProject);
                    }
                }
                catch (Exception e)
                {
                    ShowError("Error loading project:" + Environment.NewLine + e.Message);
                }
            }
        }
    }
}
