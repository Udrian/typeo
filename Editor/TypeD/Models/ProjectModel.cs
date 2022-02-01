﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TypeD.Code;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.TreeNodes;

namespace TypeD.Models
{
    public class ProjectModel : IProjectModel, IModel
    {
        // Models
        IModuleModel ModuleModel { get; set; }
        IHookModel HookModel { get; set; }
        ISaveModel SaveModel { get; set; }
        IResourceModel ResourceModel { get; set; }
        ILogModel LogModel { get; set; }

        // Providers
        IProjectProvider ProjectProvider { get; set; }
        IComponentProvider ComponentProvider { get; set; }

        // Constructors
        public ProjectModel()
        {
        }

        public void Init(IResourceModel resourceModel)
        {
            ResourceModel = resourceModel;

            ModuleModel = ResourceModel.Get<IModuleModel>();
            HookModel = ResourceModel.Get<IHookModel>();
            SaveModel = ResourceModel.Get<ISaveModel>();
            ProjectProvider = ResourceModel.Get<IProjectProvider>();
            ComponentProvider = ResourceModel.Get<IComponentProvider>();
            LogModel = ResourceModel.Get<ILogModel>();
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
            var fileToRun = Path.Combine(project.ProjectBuildOutputPath, $"{project.CSProjName}.exe");
            if(File.Exists(fileToRun))
            {
                Process.Start(fileToRun);
            }
            else
            {
                LogModel.Log($"File '{fileToRun}' does not exists, try building the project.");
            }
        }

        public void AddCode(Project project, Codalyzer code)
        {
            code.Project = project;
            code.Resources = ResourceModel;
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

        public void SetStartScene(Project project, Component scene)
        {
            //TODO: Fix
            /*
            if (scene.TypeOBaseType != "Scene") return;
            project.StartScene = scene.FullName;

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });*/
        }

        public void BuildComponentTree(Project project)
        {
            project.ComponentTree.Clear();

            var components = ComponentProvider.ListAll(project);
            foreach (var type in components)
            {
                AddComponentToTree(project, type);
            }

            HookModel.Shoot(new ComponentTreeBuiltHook(project.ComponentTree));
        }

        public bool LoadAssembly(Project project)
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

        // Internal

        private void AddComponentToTree(Project project, Component component)
        {
            var namespaces = (component.Namespace.StartsWith(project.ProjectName) ? component.Namespace.Remove(0, project.ProjectName.Length) : component.Namespace).Split('.').ToList();
            if (namespaces.Count > 0)
                namespaces.RemoveAt(0);

            TreeNode treeNode = project.ComponentTree;
            foreach (var ns in namespaces)
            {
                if (string.IsNullOrEmpty(ns)) continue;
                if (!treeNode.Contains(ns))
                {
                    treeNode.AddNode(ns, "namespace");
                }
                treeNode = treeNode.Get(ns);
            }

            if(ComponentProvider.Exists(project, component))
            {
                treeNode.AddNode(component.ClassName, component);
            }
            else
            {
                treeNode.AddNode($"*{component.ClassName}", component);
            }
        }
    }
}
