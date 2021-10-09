using Microsoft.Win32;
using TypeD;
using TypeD.Models.Data;
using TypeD.Models.Providers.Interfaces;

namespace TypeDitor.Commands.Project
{
    class OpenProjectCommand : ProjectCommands
    {
        // Providers
        private IRecentProvider RecentProvider { get; set; }

        public OpenProjectCommand(IRecentProvider recentProvider)
        {
            RecentProvider = recentProvider;
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
                openFileDialog.Filter = "TypeO projects (.typeo)|*.typeo";
                if (openFileDialog.ShowDialog() == true)
                {
                    path = openFileDialog.FileName;
                }
            }

            //Open project
            if (!string.IsNullOrEmpty(path))
            {
                var loadedProject = await Command.Project.Load(path);
                RecentProvider.Add(loadedProject.ProjectFilePath, loadedProject.ProjectName);

                this.OpenMainWindow(loadedProject);
            }
        }
    }
}
