using System;
using System.IO;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Data;
using TypeD.Helpers;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public partial class ProjectCommand
    {
        //TODO: Remove
        public static  Action<ProjectModel> InitProject;

        public async Task<ProjectModel> Create(string projectName, string location, string csSolutionPath, string csProjName, Action<int> progress)
        {
            // Validate
            if (string.IsNullOrEmpty(location)) location = @".\";
            if (string.IsNullOrEmpty(csSolutionPath)) csSolutionPath = @$".\{projectName}.sln";
            if (string.IsNullOrEmpty(csProjName)) csProjName = projectName;

            if (Path.GetFileNameWithoutExtension(location) != projectName)
            {
                location = Path.Combine(location, projectName);
            }
            progress(5);

            // Create
            var project = new ProjectModel(location, new ProjectData()
                {
                    ProjectName = projectName,
                    CSSolutionPath = csSolutionPath,
                    CSProjName = csProjName
                }
            );

            progress(10);

            if (!Directory.Exists(project.Location))
            {
                Directory.CreateDirectory(project.Location);
            }

            await CreateSolution(project);
            progress(30);
            await CreateProject(project);
            progress(50);

            // Prepare
            project.AddCode(new ProgramCode(project));
            //InitProject(project);

            var data = await Module.List();
            var coreModuleName = "TypeOCore";
            var coreModuleVersion = data.Modules["TypeOCore"][0];
            progress(60);

            await Module.Download(coreModuleName, coreModuleVersion);
            Module.Add(coreModuleName, coreModuleVersion, project);
            progress(80);

            await Save(project);
            progress(90);
            await project.Build();
            progress(100);
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
