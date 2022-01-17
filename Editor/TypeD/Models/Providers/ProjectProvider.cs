using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Code;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.DTO;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

namespace TypeD.Models.Providers
{
    public class ProjectProvider : IProjectProvider, IProvider
    {
        // Models
        IResourceModel ResourceModel { get; set; }
        IProjectModel ProjectModel { get; set; }
        IModuleModel ModuleModel { get; set; }
        IHookModel HookModel { get; set; }
        ISaveModel SaveModel { get; set; }
        IRestoreModel RestoreModel { get; set; }

        // Providers
        IModuleProvider ModuleProvider { get; set; }

        // Constructors
        public ProjectProvider()
        {
        }

        public void Init(IResourceModel resourceModel)
        {
            ResourceModel = resourceModel;

            ProjectModel = ResourceModel.Get<IProjectModel>();
            ModuleModel = ResourceModel.Get<IModuleModel>();
            HookModel = ResourceModel.Get<IHookModel>();
            SaveModel = ResourceModel.Get<ISaveModel>();
            RestoreModel = ResourceModel.Get<IRestoreModel>();

            ModuleProvider = ResourceModel.Get<IModuleProvider>();
        }

        // Functions
        public async Task<Project> Create(string projectName, string location, string csSolutionPath, string csProjName, Action<int> progress)
        {
            progress(0);
            // Validate
            if (string.IsNullOrEmpty(location)) location = @".\";
            if (string.IsNullOrEmpty(csSolutionPath)) csSolutionPath = @$".\{projectName}.sln";
            if (string.IsNullOrEmpty(csProjName)) csProjName = projectName;

            if (Path.GetFileNameWithoutExtension(location) != projectName)
            {
                location = Path.Combine(location, projectName);
            }
            progress(1);

            // Create
            var project = new Project(location, new ProjectDTO()
            {
                ProjectName = projectName,
                CSSolutionPath = csSolutionPath,
                CSProjName = csProjName
            }
            );

            progress(2);

            if (!Directory.Exists(project.Location))
            {
                Directory.CreateDirectory(project.Location);
            }

            await CreateSolution(project);
            progress(20);
            await CreateProject(project);
            progress(30);

            // Prepare
            ProjectModel.AddCode(project, new ProgramCode());

            var modulesToAdd = new List<string>() { "TypeOCore", "TypeDCore" };
            var moduleList = await ModuleProvider.List();

            progress(35);

            foreach(var moduleToAdd in modulesToAdd)
            {
                var addModuleVersion = moduleList.FirstOrDefault(m => { return m.Name == moduleToAdd; })?.Versions[0];

                var module = ModuleProvider.Create(moduleToAdd, addModuleVersion);
                ProjectModel.AddModule(project, module);
            }

            progress(50);
            //Save and load the module first so we can call the project creation hook
            await SaveModel.Save();
            progress(65);
            project = await Load(project.ProjectFilePath);
            progress(75);

            HookModel.Shoot(new ProjectCreateHook(project));
            progress(80);

            await SaveModel.Save();
            progress(90);
            await ProjectModel.Build(project);
            progress(100);
            // Return
            return project;
        }

        public async Task<Project> Load(string projectFilePath)
        {
            //TODO: Add progress bar to loading
            if (!projectFilePath.EndsWith(".typeo")) return null;

            try
            {
                return await Task.Run(async () =>
                {
                    var projectData = JSON.Deserialize<ProjectDTO>(projectFilePath);
                    var project = new Project(Path.GetDirectoryName(projectFilePath), projectData);

                    // Prepare
                    foreach(var module in project.Modules)
                    {
                        await ModuleModel.Download(module);
                        ModuleModel.LoadAssembly(module);
                    }

                    ProjectModel.LoadAssembly(project);
                    ProjectModel.BuildComponentTree(project);

                    await RestoreModel.Restore(project);

                    return project;
                });
            }
            catch
            {
                throw;
            }
        }

        public async Task Save(Project project)
        {
           await Task.Run(() =>
            {
                JSON.Serialize(new ProjectDTO()
                {
                    ProjectName = project.ProjectName,
                    CSSolutionPath = project.CSSolutionPath,
                    CSProjName = project.CSProjName,
                    Modules = project.Modules.Select(m => new ModuleDTO() { Name = m.Name, Version = m.Version }).ToList(),
                    StartScene = project.StartScene
                }, project.ProjectFilePath);
            });
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
