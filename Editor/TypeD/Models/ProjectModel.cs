using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.TreeNodes;

namespace TypeD.Models
{
    public class ProjectModel : IProjectModel
    {
        // Models
        ModuleModel ModuleModel { get; set; }
        IHookModel HookModel { get; set; }
        ISaveModel SaveModel { get; set; }
        IResourceModel Resources { get; set; }

        // Providers
        public IProjectProvider ProjectProvider { get; set; } //TODO: Should not be public
        public ITypeOTypeProvider TypeOTypeProvider { get; set; } //TODO: Should not be public

        // Constructors
        public ProjectModel(IModuleModel moduleModel, IHookModel hookModel, ISaveModel saveModel, IResourceModel resources, IProjectProvider projectProvider, ITypeOTypeProvider typeOTypeProvider)
        {
            ModuleModel = moduleModel as ModuleModel;
            HookModel = hookModel;
            SaveModel = saveModel;
            Resources = resources;
            ProjectProvider = projectProvider;
            TypeOTypeProvider = typeOTypeProvider;
        }

        // Functions
        public void AddModule(Project project, Module module)
        {
            var path = Path.Combine(project.Location, project.CSProjName, $"{project.CSProjName}.csproj");
            if (!File.Exists(path)) return;
            project.Modules.Add(module);

            var CSProj = SaveModel.GetSaveContext<XElement>("ProjectCSProj") ?? XElement.Load(path);
            ModuleModel.AddToProjectXML(module, CSProj);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
            SaveModel.AddSave("ProjectCSProj", CSProj, (context) => {
                return Task.Run(() =>
                {
                    (context as XElement).Save(path);
                });
            });
        }

        public async Task<bool> Build(Project project)
        {
            var path = Path.Combine(project.Location, project.CSSolutionPath);
            if (!File.Exists(path)) return false;

            if (project.Assembly != null)
            {
                project.Assembly = null;
            }
            await CMD.Run($"dotnet build \"{path}\" --output \"{project.ProjectBuildOutputPath}\"");

            return LoadAssembly(project);
        }

        public void Run(Project project)
        {
            Process.Start(Path.Combine(project.ProjectBuildOutputPath, $"{project.CSProjName}.exe"));
        }

        public void AddCode(Project project, Codalyzer code)
        {
            code.Project = project;
            code.Resources = Resources;
            code.Init();
            if (!code.Initialized) throw new Exception($"Codalyzer '{code.GetType().FullName}' not initialized");

            var codes = SaveModel.GetSaveContext<List<Codalyzer>>("Code") ?? new List<Codalyzer>();
            codes.Add(code);
            SaveModel.AddSave("Code", codes, 
                (context) => {
                    return Task.Run(() =>
                    {
                        foreach(var saveCode in context as List<Codalyzer>)
                        {
                            saveCode.Generate();
                            saveCode.Save();
                        }
                    });
                });
        }

        public void SetStartScene(Project project, TypeOType scene)
        {
            //TODO: Fix
            /*
            if (scene.TypeOBaseType != "Scene") return;
            project.StartScene = scene.FullName;

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });*/
        }

        public void BuildTypeOTypeTree(Project project)
        {
            project.TypeOTypeTree.Clear();

            var typeOTypes = TypeOTypeProvider.ListAll(project);
            foreach (var type in typeOTypes)
            {
                AddTypeToTree(project, type);
            }

            HookModel.Shoot("TypeTreeBuilt", new TypeTreeBuiltHook(project.TypeOTypeTree));
        }

        //Internal functions
        internal bool LoadAssembly(Project project)
        {
            var path = Path.Combine(project.ProjectBuildOutputPath, $"{project.CSProjName}.dll");
            if (!File.Exists(path)) return false;

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);

                project.Assembly = System.Reflection.Assembly.Load(bytes);
            }

            return true;
        }

        private void AddTypeToTree(Project project, TypeOType typeOType)
        {
            var namespaces = (typeOType.Namespace.StartsWith(project.ProjectName) ? typeOType.Namespace.Remove(0, project.ProjectName.Length) : typeOType.Namespace).Split('.').ToList();
            if (namespaces.Count > 0)
                namespaces.RemoveAt(0);

            TreeNode treeNode = project.TypeOTypeTree;
            //TODO: Need to display folder or namespace nodes in a specific way
            foreach (var ns in namespaces)
            {
                if (string.IsNullOrEmpty(ns)) continue;
                if (!treeNode.Contains(ns))
                {
                    treeNode.AddNode(ns, "namespace");
                }
                treeNode = treeNode.Get(ns);
            }

            if(TypeOTypeProvider.Exists(project, typeOType))
            {
                treeNode.AddNode(typeOType.ClassName, typeOType.TypeOBaseType);
            }
            else
            {
                treeNode.AddNode($"*{typeOType.ClassName}", typeOType.TypeOBaseType);
            }
        }
    }
}
