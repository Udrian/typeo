﻿using System.IO;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Data;
using TypeD.Helpers;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static async Task<ProjectModel> Create(string projectName, string location = null, string csSolutionPath = null, string csProjName = null)
        {
            // Validate
            if (string.IsNullOrEmpty(location)) location = @".\";
            if (string.IsNullOrEmpty(csSolutionPath)) csSolutionPath = @$".\{projectName}.sln";
            if (string.IsNullOrEmpty(csProjName)) csProjName = projectName;

            if (Path.GetFileNameWithoutExtension(location) != projectName)
            {
                location = Path.Combine(location, projectName);
            }

            // Create
            var project = new ProjectModel(location, new ProjectData()
                {
                    ProjectName = projectName,
                    CSSolutionPath = csSolutionPath,
                    CSProjName = csProjName
                }
            );

            if (!Directory.Exists(project.Location))
            {
                Directory.CreateDirectory(project.Location);
            }

            await CreateSolution(project);
            await CreateProject(project);

            // Prepare
            project.AddCode(new ProgramCode(project));
            project.AddCode(new GameCode(project));

            project.AddModule(new ModuleModel("TypeOCore"));
            project.AddModule(new ModuleModel("TypeODesktop"));
            project.AddModule(new ModuleModel("TypeOSDL"));

            project.Save();
            await project.Build();
            // Return
            return project;
        }

        private static async Task CreateSolution(ProjectModel project)
        {
            if (!File.Exists(Path.Combine(project.Location, project.CSSolutionPath)))
            {
                await CMD.Run(new string[] {
                    $"cd \"{project.Location}\"",
                    $"dotnet new sln --name \"{Path.GetFileNameWithoutExtension(project.CSSolutionPath)}\""
                });
            }
        }

        private static async Task CreateProject(ProjectModel project)
        {
            if (!File.Exists(Path.Combine(project.Location, project.ProjectName, $"{project.ProjectName}.csproj")))
            {
                await CMD.Run(new string[]
                {
                    $"cd \"{project.Location}\"",
                    $"dotnet new console -lang \"C#\" -n \"{project.CSProjName}\"",
                    $"dotnet sln \"{Path.GetFileName(project.CSSolutionPath)}\" add \"{project.CSProjName}\""
                });
            }
        }
    }
}