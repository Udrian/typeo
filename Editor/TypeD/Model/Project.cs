using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using TypeD.Helper;
using TypeD.Model.Code;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeD.Model
{
    public class Project
    {
        public static Project Create(string name, string location, string solutionName, string csProjName)
        {
            if(string.IsNullOrEmpty(location)) location = @".\";
            var project = new Project()
            {
                Name = name,
                Location = location,
                SolutionFilePath = $@".\{solutionName}.sln",
                CSProjectName = csProjName
            };
            project.AddCodeFiles();
            return project;
        }

        public static Project Load(string filePath)
        {
            try
            {
                var project = JSON.Deserialize<Project>(filePath);
                project.Location = Path.GetDirectoryName(filePath);
                project.AddCodeFiles();
                return project;
            }
            catch
            {
                return null;
            }
        }

        public string Name { get; set; }
        public string SolutionFilePath { get; set; }
        public string CSProjectName { get; set; }

        public string ProjectFilePath { get { return $@"{Location}\{Name}.typeo"; } }
        public string ProjectTypeO { get { return Path.Combine(Location, "typeo"); } }
        public string ProjectBuildOutput { get { return Path.Combine(ProjectTypeO, "build", CSProjectName); } }
        public string Location { get; private set; }
        public Assembly Assembly { get; private set; }
        public List<TypeInfo> Types { get; private set; }
        public Dictionary<string, Codalyzer> Codes { get; private set; }

        protected Project()
        {
            Codes = new Dictionary<string, Codalyzer>();
        }

        private void AddCodeFiles()
        {
            Types = new List<TypeInfo>();
            AddCode(new ProgramCode(this));
            AddCode(new GameCode(this));
        }

        // TODO: ADD EXCISTING PROJECT
        /*private string ConstructPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return "";

            if (IsDirectory(filePath))
                return Path.Combine(filePath, $"{Name}.typeo");
            return filePath;
        }*/

        // TODO: ADD EXCISTING PROJECT
        /*public bool ScanForSolution()
        {
            if (string.IsNullOrEmpty(ProjectDirPath)) return false;
            if (!Directory.Exists(ProjectDirPath)) return false;

            var solutionPath = "";
            foreach(var file in Directory.GetFiles(ProjectDirPath))
            {
                if (file.EndsWith(".sln"))
                {
                    solutionPath = file;
                    break;
                }
            }
            if (string.IsNullOrEmpty(solutionPath)) return false;

            SolutionFilePath = $".{solutionPath.Substring(ProjectDirPath.Length)}";//.TrimStart('/').TrimStart('\\');
            return true;
        }*/


        // TODO: ADD EXCISTING PROJECT
        /*public void ConstructSolutionPath()
        {
            SolutionFilePath = $@".\{Name}.sln";
        }*/


        // TODO: ADD EXCISTING PROJECT
        /*public List<string> FetchProjectsFromSolution()
        {
            var projects = new List<string>();
            if (string.IsNullOrEmpty(SolutionFilePath)) return projects;
            var path = Path.Combine(ProjectDirPath, SolutionFilePath);
            if (!File.Exists(path)) return projects;

            var lines = File.ReadAllLines(path);
            foreach(var line in lines)
            {
                if(line.StartsWith("Project("))
                {
                    var firstSeperator = "\") = \"";
                    var secondSeperator = "\", \"";
                    var firstIndex = line.IndexOf(firstSeperator) + firstSeperator.Length;
                    var length = line.IndexOf(secondSeperator) - firstIndex;
                    var project = line.Substring(firstIndex, length);

                    projects.Add(project);
                }
            }

            return projects;
        }*/

        public bool Save()
        {
            JSON.Serialize(new { 
                Name = Name,
                SolutionFilePath = SolutionFilePath,
                CSProjectName = CSProjectName
            }, ProjectFilePath);

            return true;
        }

        public async Task CreateSolution()
        {
            await CMD.Run(new string[] {
                $"cd \"{Location}\"",
                $"dotnet new sln --name \"{Path.GetFileNameWithoutExtension(SolutionFilePath)}\""
            });
        }

        public async Task CreateProject()
        {
            await CMD.Run(new string[]
            {
                $"cd \"{Location}\"",
                $"dotnet new console -lang \"C#\" -n \"{CSProjectName}\"",
                $"dotnet sln \"{Path.GetFileName(SolutionFilePath)}\" add \"{CSProjectName}\""
            });
        }

        public void AddModule(Module module)
        {
            var path = Path.Combine(Location, CSProjectName, $"{CSProjectName}.csproj");
            if (!File.Exists(path)) return;

            module.CopyProject(ProjectTypeO);
            var projectX = XElement.Load(path);
            module.AddToProjectXML(projectX);
            projectX.Save(path);
        }

        public async Task<bool> Build()
        {
            var path = Path.Combine(Location, SolutionFilePath);
            if (!File.Exists(path)) return false;

            if (Assembly != null)
            {
                Assembly = null;
                Types.Clear();
            }
            await CMD.Run($"dotnet build \"{path}\" --output \"{ProjectBuildOutput}\"");

            return true;
        }

        public bool LoadAssembly()
        {
            var path = Path.Combine(ProjectBuildOutput, $"{CSProjectName}.dll");
            if (!File.Exists(path)) return false;

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);

                Assembly = Assembly.Load(bytes);
            }

            return true;
        }

        public bool LoadTypes()
        {
            if (Assembly == null) return false;
                
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

            return true;
        }

        public void GenerateProjectFiles()
        {
            Codes[$"{Name}.Program"].Generate(Location);
            Codes[$"{Name}.{Name}Game"].Generate(Location);
        }

        public void Run()
        {
            Process.Start(Path.Combine(ProjectBuildOutput, $"{CSProjectName}.exe"));
        }

        public void AddCode(Codalyzer code)
        {
            Codes.Add($"{code.Namespace}.{code.Name}", code);
        }
    }
}
