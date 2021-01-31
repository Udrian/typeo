﻿using System.Threading.Tasks;
using TypeD.Commands.Project;
using TypeD.Models;

namespace TypeDEditor.Controller
{
    public class FileController
    {
        public static ProjectModel LoadedProject { get; set; }

        public async Task Create(string projectName, string location, string csSolutionPath, string csProjName)
        {
            if (LoadedProject != null)
                ProjectCommand.Clear(LoadedProject);
            LoadedProject = await ProjectCommand.Create(projectName, location, csSolutionPath, csProjName);
        }

        public async Task Open(string projectFilePath)
        {
            if (LoadedProject != null)
                ProjectCommand.Clear(LoadedProject);
            LoadedProject = await ProjectCommand.Load(projectFilePath);
        }
        public void Save()
        {
            if (LoadedProject == null) return;
                ProjectCommand.Save(LoadedProject);
        }
    }
}
