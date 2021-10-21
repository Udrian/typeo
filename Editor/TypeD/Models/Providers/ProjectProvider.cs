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
    public class ProjectProvider : IProjectProvider
    {
        // Models
        private ProjectModel ProjectModel { get; set; }
        private ModuleModel ModuleModel { get; set; }
        private IHookModel HookModel { get; set; }
        private ISaveModel SaveModel { get; set; }

        // Providers
        private IModuleProvider ModuleProvider { get; set; }

        // Constructors
        public ProjectProvider(
            IProjectModel projectModel, IModuleModel moduleModel, IHookModel hookModel, ISaveModel saveModel,
                                        IModuleProvider moduleProvider
            )
        {
            ProjectModel = projectModel as ProjectModel;
            ModuleModel = moduleModel as ModuleModel;
            HookModel = hookModel;
            SaveModel = saveModel;
            ModuleProvider = moduleProvider;
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

            var modulesToAdd = new List<string>() { "TypeOCore", "TypeDCore" };
            var moduleList = await ModuleProvider.List();

            progress(60);

            foreach(var moduleToAdd in modulesToAdd)
            {
                var addModuleVersion = moduleList.FirstOrDefault(m => { return m.Name == moduleToAdd; })?.Versions[0];

                var module = ModuleProvider.Add(moduleToAdd, addModuleVersion);
                await ModuleModel.Download(module);

                ProjectModel.AddModule(project, module);
                ModuleModel.InitializeTypeD(module);
            }

            progress(75);
            HookModel.Shoot("ProjectCreate", new ProjectCreateHook(project));
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
            if (!projectFilePath.EndsWith(".typeo")) return null;

            try
            {
                return await Task.Run(async () =>
                {
                    var projectData = JSON.Deserialize<ProjectDTO>(projectFilePath);
                    var project = new Project(Path.GetDirectoryName(projectFilePath), projectData);

                    // Prepare
                    ProjectModel.AddCode(project, new ProgramCode(project));

                    foreach(var module in project.Modules)
                    {
                        await ModuleModel.Download(module);
                        ModuleModel.LoadAssembly(module);
                        ModuleModel.InitializeTypeD(module);
                    }

                    ProjectModel.LoadAssembly(project);

                    return project;
                });
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
