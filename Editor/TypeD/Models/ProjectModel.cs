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
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeD.Models
{
    public class ProjectModel
    {
        // Data
        public string ProjectName { get; private set; }
        public string CSSolutionPath { get; private set; }
        public string CSProjName { get; private set; }
        public List<ModuleModel> Modules { get; private set; }
        public string Location { get; private set; }

        public TreeNode Nodes { get; private set; }

        // Loads
        public Dictionary<string, Codalyzer> Codes { get; private set; }
        public Assembly Assembly { get; private set; }
        public List<TypeInfo> Types { get; private set; }

        // Constructors
        internal ProjectModel(string location, ProjectData projectData)
        {
            Location = location;

            Codes = new Dictionary<string, Codalyzer>();
            Types = new List<TypeInfo>();

            ProjectName = projectData.ProjectName;
            CSSolutionPath = projectData.CSSolutionPath;
            CSProjName = projectData.CSProjName;
            Modules = projectData.Modules.Select(m => new ModuleModel(m)).ToList();

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

            var programCode = Codes[$"{ProjectName}.Program"];
            if (module.ModuleTypeInfo != null)
                programCode.Usings.Add(module.ModuleTypeInfo.Namespace);

            module.CopyProject(ProjectTypeO);
            var projectX = XElement.Load(path);
            module.AddToProjectXML(projectX);
            projectX.Save(path);
        }

        public async Task<bool> Build()
        {
            var path = Path.Combine(Location, CSSolutionPath);
            if (!File.Exists(path)) return false;

            if (Assembly != null)
            {
                Assembly = null;
                Types.Clear();
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
                if (type.IsSubclassOf(typeof(Game)) ||
                    type.IsSubclassOf(typeof(Scene)) ||
                    type.IsSubclassOf(typeof(Entity)) ||
                    type.IsSubclassOf(typeof(Stub)) ||
                    type.IsSubclassOf(typeof(Logic)) ||
                    type.IsSubclassOf(typeof(Drawable)) ||
                    type.IsSubclassOf(typeof(EntityData)))
                {
                    Types.Add(type);
                }
            }

            if (Nodes != null)
                Nodes.Clear();
            Nodes = TreeNode.Create(ProjectName, this, ProjectName);

            foreach (var type in Types)
            {
                AddTypeToTree(type);
            }

            return true;
        }

        private void AddTypeToTree(TypeInfo type)
        {
            var namespaces = (type.Namespace.StartsWith(ProjectName) ? type.Namespace.Remove(0, ProjectName.Length) : type.Namespace).Split('.');

            var node = Nodes;
            foreach (var ns in namespaces)
            {
                if (string.IsNullOrEmpty(ns)) continue;
                if (!node.Nodes.ContainsKey(ns))
                    node.AddNode(ns, null, ns);
                node = node.Nodes[ns];
            }

            node.AddNode(type.Name, type, type.FullName);
        }

        public void Run()
        {
            Process.Start(Path.Combine(ProjectBuildOutput, $"{CSProjName}.exe"));
        }

        public void AddCode(Codalyzer code)
        {
            Codes.Add($"{code.Namespace}.{code.Name}", code);
        }

        public void Save()
        {
            JSON.Serialize(new ProjectData()
            {
                ProjectName = ProjectName,
                CSSolutionPath = CSSolutionPath,
                CSProjName = CSProjName,
                Modules = Modules.Select(m => m.Name).ToList()
            }, ProjectFilePath);

            foreach (var code in Codes.Values)
            {
                code.Generate();
                code.Save(Location);
            }
        }
    }
}
