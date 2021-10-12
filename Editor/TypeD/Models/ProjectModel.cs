using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TypeD.Code;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;
using TypeD.TreeNodes;
using TypeD.Types;

namespace TypeD.Models
{
    public class ProjectModel : IProjectModel
    {
        // Models
        public ModuleModel ModuleModel { get; set; }
        public IHookModel HookModel { get; set; }
        public ISaveModel SaveModel { get; set; }

        // Providers
        public IProjectProvider ProjectProvider { get; set; }

        // Constructors
        public ProjectModel(IModuleModel moduleModel, IHookModel hookModel, ISaveModel saveModel, IProjectProvider projectProvider)
        {
            ModuleModel = moduleModel as ModuleModel;
            HookModel = hookModel;
            SaveModel = saveModel;
            ProjectProvider = projectProvider;
        }

        // Functions
        public void AddModule(Project project, Module module)
        {
            var path = Path.Combine(project.Location, project.CSProjName, $"{project.CSProjName}.csproj");
            if (!File.Exists(path)) return;
            project.Modules.Add(module);

            var programCode = project.TypeOTypes[$"{project.ProjectName}.Program"];
            if (module.ModuleTypeInfo != null)
                programCode.Codes.First().Usings.Add(module.ModuleTypeInfo.Namespace);

            if(project.CSProj == null)
            {
                project.CSProj = XElement.Load(path);
            }
            ModuleModel.AddToProjectXML(module, project.CSProj);

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
            SaveModel.AddSave("ProjectCSProj", () => {
                return Task.Run(() =>
                {
                    project.CSProj.Save(path);
                    project.CSProj = null;
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
            await CMD.Run($"dotnet build \"{path}\" --output \"{project.ProjectBuildOutput}\"");

            return LoadAssembly(project);
        }

        public void Run(Project project)
        {
            Process.Start(Path.Combine(project.ProjectBuildOutput, $"{project.CSProjName}.exe"));
        }

        public void AddCode(Project project, Codalyzer code, string typeOBaseType = "")
        {
            var key = $"{code.Namespace}.{code.ClassName}";
            if (!project.TypeOTypes.ContainsKey(key))
            {
                RegisterType(project, code.BaseClass, code.ClassName, code.Namespace, null);
            }
            project.TypeOTypes[key].Codes.Add(code);
            if (typeOBaseType != "")
            {
                project.TypeOTypes[key].TypeOBaseType = typeOBaseType;
            }
        }

        public List<TypeOType> GetTypeFromName(Project project, string name)
        {
            var types = new List<TypeOType>();

            if (name != null && project.TypeOTypes.ContainsKey(name))
            {
                types.Add(project.TypeOTypes[name]);
            }
            else
            {
                foreach (var typeDType in project.TypeOTypes.Values)
                {
                    if (typeDType.ClassName == name)
                    {
                        types.Add(typeDType);
                    }
                }
            }

            return types;
        }

        public void Clear(Project project)
        {
            project.Tree.Clear();
        }

        public void SetStartScene(Project project, TypeOType scene)
        {
            if (scene.TypeOBaseType != "Scene") return;
            project.StartScene = scene.FullName;

            SaveModel.AddSave("Project", () => { return ProjectProvider.Save(project); });
        }

        public void BuildTree(Project project)
        {
            if (project.Tree == null)
            {
                project.Tree = new Tree();
            }
            else
            {
                project.Tree.Clear();
            }
            project.Tree.AddNode(project.ProjectName, null);

            foreach (var type in project.TypeOTypes.Values)
            {
                AddTypeToTree(project, type);
            }
        }

        //Internal functions
        internal bool LoadAssembly(Project project)
        {
            var path = Path.Combine(project.ProjectBuildOutput, $"{project.CSProjName}.dll");
            if (!File.Exists(path)) return false;

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);

                project.Assembly = System.Reflection.Assembly.Load(bytes);
            }

            foreach (var type in project.Assembly.DefinedTypes)
            {
                var typeDType = TypeOType.GetBaseTypeOClassName(type);
                if (typeDType == "") continue;

                RegisterType(project, typeDType, null, null, type);
            }

            BuildTree(project);

            return true;
        }

        private void AddTypeToTree(Project project, TypeOType typeOType)
        {
            if (typeOType.Codes.FirstOrDefault()?.ClassName == "Program") return;

            var namespaces = (typeOType.Namespace.StartsWith(project.ProjectName) ? typeOType.Namespace.Remove(0, project.ProjectName.Length) : typeOType.Namespace).Split('.').ToList();
            if (namespaces.Count > 0)
                namespaces.RemoveAt(0);

            TreeNode treeNode = project.Tree;
            foreach (var ns in namespaces)
            {
                if (string.IsNullOrEmpty(ns)) continue;
                if (!treeNode.Contains(ns))
                {
                    treeNode.AddNode(ns, null);
                }
                treeNode = treeNode.Get(ns);
            }

            if (typeOType.TypeInfo != null)
            {
                treeNode.AddNode(typeOType.ClassName, new ItemCode(typeOType));
            }
            else
            {
                treeNode.AddNode($"*{typeOType.ClassName}", new ItemCode(typeOType));
            }
        }

        private void RegisterType(Project project, string typeOBaseType, string classname, string @namespace, System.Reflection.TypeInfo typeInfo)
        {
            var typeOType = TypeOType.InstantiateTypeOType(typeOBaseType, classname, @namespace, typeInfo, project);
            if (typeOType == null) typeOType = new ProgramTypeOType() { ClassName = classname, Namespace = @namespace, Project = project, TypeOBaseType = typeOBaseType };//TODO: Remove this
            var key = typeOType.FullName;
            if (!project.TypeOTypes.ContainsKey(key))
            {
                project.TypeOTypes.Add(key, typeOType);
            }
        }
    }
}
