﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.DTO;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

namespace TypeD.Models.Providers
{
    public class ProjectProvider : IProjectProvider
    {
        //TODO: Remove
        public static Action<ProjectModel> InitProject;

        // Models
        private ProjectModel ProjectModel { get; set; }
        private ModuleModel ModuleModel { get; set; }

        // Providers
        private ModuleProvider ModuleProvider { get; set; }

        // Constructors
        public ProjectProvider(
            IProjectModel projectModel, IModuleModel moduleModel,
                                        IModuleProvider moduleProvider
            )
        {
            ProjectModel = projectModel as ProjectModel;
            ModuleModel = moduleModel as ModuleModel;
            ModuleProvider = moduleProvider as ModuleProvider;
        }

        // Functions
        public async Task<Project> Create(string projectName, string location, string csSolutionPath, string csProjName, Action<int> progress)
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
            var project = new Project(location, new ProjectDTO()
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
            ProjectModel.AddCode(project, new ProgramCode(project));
            //InitProject(project);

            var modulesToAdd = new List<string>() { "TypeOCore", "TypeDCore" };
            var moduleList = await ModuleProvider.List();

            progress(60);

            foreach(var moduleToAdd in modulesToAdd)
            {
                var addModuleVersion = moduleList.FirstOrDefault(m => { return m.Name == moduleToAdd; })?.Versions[0];

                var module = ModuleProvider.Add(moduleToAdd, addModuleVersion);
                await ModuleModel.Download(module);

                ProjectModel.AddModule(project, module);
            }

            progress(80);

            await Save(project);
            progress(90);
            await ProjectModel.Build(project);
            progress(100);
            // Return
            return project;
        }

        public async Task<Project> Load(string projectFilePath)
        {
            if (!projectFilePath.EndsWith(".typeo")) return null;

            try
            {
                var task = new Task<Project>(() =>
                {
                    var projectData = JSON.Deserialize<ProjectDTO>(projectFilePath);
                    var project = new Project(Path.GetDirectoryName(projectFilePath), projectData);

                    // Prepare
                    ProjectModel.AddCode(project, new ProgramCode(project));
                    ProjectModel.LoadAssembly(project);
                    ProjectModel.BuildTree(project);

                    return project;
                });

                task.Start();
                return await task;
            }
            catch
            {
                return null;
            }
        }


        public async Task Save(Project project)
        {
            var task = new Task(() =>
            {
                JSON.Serialize(new ProjectDTO()
                {
                    ProjectName = project.ProjectName,
                    CSSolutionPath = project.CSSolutionPath,
                    CSProjName = project.CSProjName,
                    Modules = project.Modules.Select(m => new ModuleDTO() { Name = m.Name, Version = m.Version }).ToList(),
                    StartScene = project.StartScene
                }, project.ProjectFilePath);

                foreach (var typeDType in project.TypeOTypes.Values)
                {
                    typeDType.Save();
                }
            });
            task.Start();
            await task;
        }

        // Internal
        private async Task CreateSolution(Project project)
        {
            if (!File.Exists(Path.Combine(project.Location, project.CSSolutionPath)))
            {
                await CMD.Run(new string[] {
                    $"cd \"{project.Location}\"",
                    $"dotnet new sln --name \"{Path.GetFileNameWithoutExtension(project.CSSolutionPath)}\""
                });
            }
        }

        private async Task CreateProject(Project project)
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
