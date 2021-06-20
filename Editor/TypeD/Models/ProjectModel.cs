using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using TypeD.Code;
using TypeD.Data;
using TypeD.Helpers;
using TypeD.TreeNodes;
using TypeD.Types;

namespace TypeD.Models
{
    public class ProjectModel
    {
        // Data
        public string ProjectName { get; private set; }
        public string CSSolutionPath { get; private set; }
        public string CSProjName { get; private set; }
        public List<ModuleModel> Modules { get; private set; }
        public string StartScene { get; set; }
        public string Location { get; private set; }

        public Tree Tree { get; private set; }

        // Loads
        public Dictionary<string, TypeOType> TypeOTypes { get; private set; }
        public Assembly Assembly { get; private set; }

        // Constructors
        internal ProjectModel(string location, ProjectData projectData)
        {
            Location = location;

            TypeOTypes = new Dictionary<string, TypeOType>();

            ProjectName = projectData.ProjectName;
            CSSolutionPath = projectData.CSSolutionPath;
            CSProjName = projectData.CSProjName;
            Modules = projectData.Modules.Select(m => new ModuleModel(m.Name, m.Version)).ToList();
            StartScene = projectData.StartScene;

            LoadAssembly();
        }

        // Paths
        public string ProjectFilePath { get { return $@"{Location}\{ProjectName}.typeo"; } }
        public string ProjectTypeO { get { return Path.Combine(Location, "typeo"); } }
        public string ProjectBuildOutput { get { return Path.Combine(ProjectTypeO, "build", CSProjName); } }

        // Functions
        public void AddModule(ModuleModel module)
        {
            var path = Path.Combine(Location, CSProjName, $"{CSProjName}.csproj");
            if (!File.Exists(path)) return;
            Modules.Add(module);

            var programCode = TypeOTypes[$"{ProjectName}.Program"];
            if (module.ModuleTypeInfo != null)
                programCode.Codes.First().Usings.Add(module.ModuleTypeInfo.Namespace);

            var projectX = XElement.Load(path);
            module.AddToProjectXML(projectX);
            //TODO: Should only save when we press save
            projectX.Save(path);
        }

        public async Task<bool> Build()
        {
            var path = Path.Combine(Location, CSSolutionPath);
            if (!File.Exists(path)) return false;

            if (Assembly != null)
            {
                Assembly = null;
            }
            await CMD.Run($"dotnet build \"{path}\" --output \"{ProjectBuildOutput}\"");

            return LoadAssembly();
        }

        private bool LoadAssembly()
        {
            var path = Path.Combine(ProjectBuildOutput, $"{CSProjName}.dll");
            if (!File.Exists(path)) return false;

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);

                Assembly = Assembly.Load(bytes);
            }

            foreach (var type in Assembly.DefinedTypes)
            {
                var typeDType = TypeOType.GetBaseTypeOClassName(type);
                if (typeDType == "") continue;

                RegisterType(typeDType, null, null, type);
            }

            BuildTree();

            return true;
        }

        public void BuildTree()
        {
            if (Tree == null)
            {
                Tree = new Tree();
            }
            else
            {
                Tree.Clear();
            }
            Tree.AddNode(ProjectName, null);

            foreach (var type in TypeOTypes.Values)
            {
                AddTypeToTree(type);
            }
        }

        private void AddTypeToTree(TypeOType typeOType)
        {
            if (typeOType.Codes.FirstOrDefault()?.ClassName == "Program") return;

            var namespaces = (typeOType.Namespace.StartsWith(ProjectName) ? typeOType.Namespace.Remove(0, ProjectName.Length) : typeOType.Namespace).Split('.').ToList();
            if (namespaces.Count > 0)
                namespaces.RemoveAt(0);

            TreeNode treeNode = Tree;
            foreach (var ns in namespaces)
            {
                if (string.IsNullOrEmpty(ns)) continue;
                if(!treeNode.Contains(ns))
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

        public void Run()
        {
            Process.Start(Path.Combine(ProjectBuildOutput, $"{CSProjName}.exe"));
        }

        public void AddCode(Codalyzer code, string typeOBaseType = "")
        {
            var key = $"{code.Namespace}.{code.ClassName}";
            if (!TypeOTypes.ContainsKey(key))
            {
                RegisterType(code.BaseClass, code.ClassName, code.Namespace, null);
            }
            TypeOTypes[key].Codes.Add(code);
            if(typeOBaseType != "")
            {
                TypeOTypes[key].TypeOBaseType = typeOBaseType;
            }
        }

        public void RegisterType(string typeOBaseType, string classname, string @namespace, TypeInfo typeInfo)
        {
            var typeOType = TypeOType.InstantiateTypeOType(typeOBaseType, classname, @namespace, typeInfo, this);
            if (typeOType == null) typeOType = new ProgramTypeOType() { ClassName = classname, Namespace = @namespace, Project = this, TypeOBaseType = typeOBaseType };//TODO: Remove this
            var key = typeOType.FullName;
            if (!TypeOTypes.ContainsKey(key))
            {
                TypeOTypes.Add(key, typeOType);
            }
        }

        public List<TypeOType> GetTypeFromName(string name)
        {
            var types = new List<TypeOType>();

            if(name != null && TypeOTypes.ContainsKey(name))
            {
                types.Add(TypeOTypes[name]);
            } 
            else
            {
                foreach (var typeDType in TypeOTypes.Values)
                {
                    if (typeDType.ClassName == name)
                    {
                        types.Add(typeDType);
                    }
                }
            }

            return types;
        }

        public async Task Save()
        {
            var task = new Task(() =>
            {
                JSON.Serialize(new ProjectData()
                {
                    ProjectName = ProjectName,
                    CSSolutionPath = CSSolutionPath,
                    CSProjName = CSProjName,
                    Modules = Modules.Select(m => new ModuleData() { Name = m.Name, Version = m.Version }).ToList(),
                    StartScene = StartScene
                }, ProjectFilePath);

                foreach (var typeDType in TypeOTypes.Values)
                {
                    typeDType.Save();
                }
            });
            task.Start();
            await task;
        }
    }
}
