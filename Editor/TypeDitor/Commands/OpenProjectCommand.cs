using Microsoft.Win32;
using System;
using TypeD.Models.Data;
using TypeD.Models.Providers.Interfaces;

namespace TypeDitor.Commands
{
    class OpenProjectCommand : ProjectCommands
    {
        // Providers
        private IRecentProvider RecentProvider { get; set; }
        private IProjectProvider ProjectProvider { get; set; }

        public OpenProjectCommand(IRecentProvider recentProvider, IProjectProvider projectProvider)
        {
            RecentProvider = recentProvider;
            ProjectProvider = projectProvider;
        }

        public async override void Execute(object param)
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
                    var loadedProject = await ProjectProvider.Load(path);
                    if (loadedProject != null)
                    {
                        RecentProvider.Add(loadedProject.ProjectFilePath, loadedProject.ProjectName);

                        this.OpenMainWindow(loadedProject);
                    }
                }
                catch (Exception e)
                {
                    this.ShowError("Error loading project:" + Environment.NewLine + e.Message);
                }
            }
        }
    }
}
