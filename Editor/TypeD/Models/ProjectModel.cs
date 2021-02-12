using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using TypeD.Data;
using TypeD.Helpers;
using TypeD.Models.TreeNodes;

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

        public TreeNode Nodes { get; private set; }

        // Loads
        public Dictionary<string, TypeDType> TypeDTypes { get; private set; }
        public Assembly Assembly { get; private set; }

        // Constructors
        internal ProjectModel(string location, ProjectData projectData)
        {
            Location = location;

            TypeDTypes = new Dictionary<string, TypeDType>();

            ProjectName = projectData.ProjectName;
            CSSolutionPath = projectData.CSSolutionPath;
            CSProjName = projectData.CSProjName;
            Modules = projectData.Modules.Select(m => new ModuleModel(m)).ToList();
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

            var programCode = TypeDTypes[$"{ProjectName}.Program"];
            if (module.ModuleTypeInfo != null)
                programCode.Codes.First().Usings.Add(module.ModuleTypeInfo.Namespace);

            module.CopyProject(ProjectTypeO);
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
                var typeDType = TypeDType.SubclasToTypeDType(type);
                if (!typeDType.HasValue) continue;

                AddType(typeDType.Value, type);
            }

            BuildTree();

            return true;
        }

        public void BuildTree()
        {
            if (Nodes == null)
            {
                Nodes = TreeNode.Create();
            }
            else
            {
                Nodes.Clear();
            }
            Nodes.AddNode(ProjectName, this);

            foreach (var type in TypeDTypes.Values)
            {
                AddTypeToTree(type);
            }
        }

        private void AddTypeToTree(TypeDType typeDType)
        {
            if (typeDType.Codes.FirstOrDefault()?.ClassName == "Program") return;

            var namespaces = (typeDType.Namespace.StartsWith(ProjectName) ? typeDType.Namespace.Remove(0, ProjectName.Length) : typeDType.Namespace).Split('.').ToList();
            if (namespaces.Count > 0)
                namespaces.RemoveAt(0);

            var node = Nodes;
            foreach (var ns in namespaces)
            {
                if (string.IsNullOrEmpty(ns)) continue;
                if (!node.Nodes.ContainsKey(ns))
                    node.AddNode(ns, null, ns);
                node = node.Nodes[ns];
            }

            if (typeDType.TypeInfo != null)
            {
                node.AddNode(typeDType.Name, typeDType, typeDType.FullName);
            }
            else
            {
                node.AddNode($"*{typeDType.Name}", typeDType, typeDType.FullName);
            }
        }

        public void Run()
        {
            Process.Start(Path.Combine(ProjectBuildOutput, $"{CSProjName}.exe"));
        }

        public void AddCode(Codalyzer code)
        {
            var key = $"{code.Namespace}.{code.ClassName}";
            if (!TypeDTypes.ContainsKey(key))
            {
                TypeDTypes.Add(key, new TypeDType()
                {
                    Name = code.ClassName,
                    Namespace = code.Namespace
                });
            }
            TypeDTypes[key].Codes.Add(code);
        }

        public void AddType(TypeDTypeType typeType, TypeInfo typeInfo)
        {
            var key = typeInfo.FullName;
            if (!TypeDTypes.ContainsKey(key))
            {
                TypeDTypes.Add(key, new TypeDType()
                {
                    Name = typeInfo.Name,
                    Namespace = typeInfo.Namespace
                });
            }
            TypeDTypes[key].TypeInfo = typeInfo;
            TypeDTypes[key].TypeType = typeType;
        }

        public List<TypeDType> GetTypeFromName(string name)
        {
            var types = new List<TypeDType>();

            if(TypeDTypes.ContainsKey(name))
            {
                types.Add(TypeDTypes[name]);
            } 
            else
            {
                foreach (var typeDType in TypeDTypes.Values)
                {
                    if (typeDType.Name == name)
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
                    Modules = Modules.Select(m => m.Name).ToList(),
                    StartScene = StartScene
                }, ProjectFilePath);

                foreach (var typeDType in TypeDTypes.Values)
                {
                    foreach (var code in typeDType.Codes)
                    {
                        code.Generate();
                        code.Save(Location);
                    }
                }
            });
            task.Start();
            await task;
        }
    }
}
