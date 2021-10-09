using Microsoft.Win32;
using TypeD;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeDitor.Commands.Project
{
    class OpenProjectCommand : ProjectCommands
    {
        private IRecentModel RecentModel { get; set; }

        public OpenProjectCommand(IRecentModel recentModel)
        {
            RecentModel = recentModel;
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
                RecentModel.Add(loadedProject.ProjectFilePath, loadedProject.ProjectName);

                this.OpenMainWindow(loadedProject);
            }
        }
    }
}
