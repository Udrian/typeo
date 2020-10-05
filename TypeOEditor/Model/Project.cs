using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TypeOEditor.Helper;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeOEditor.Model
{
    public class Project
    {
        public static Project Create(string name, string filePath)
        {
            var project = new Project()
            {
                Name = name
            };
            project.ProjectFilePath = project.ConstructPath(filePath);
            return project;
        }

        public static Project Load(string filePath)
        {
            try
            {
                var project = JSON.Deserialize<Project>(filePath);
                project.ProjectFilePath = filePath;
                return project;
            }
            catch
            {
                return null;
            }
        }

        public string Name { get; set; }
        public string SolutionFilePath { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDLLPath { get; set; }

        public string ProjectFilePath { get; private set; }
        public string ProjectDirPath { get { return IsDirectory(ProjectFilePath) ? ProjectFilePath : Path.GetDirectoryName(ProjectFilePath); } }
        public Assembly Assembly { get; private set; }
        public TypeInfo Game { get; private set; }
        public List<TypeInfo> Scenes { get; private set; }
        public List<TypeInfo> Entities { get; private set; }
        public List<TypeInfo> Stubs { get; private set; }
        public List<TypeInfo> Logics { get; private set; }
        public List<TypeInfo> Drawables { get; private set; }
        public List<TypeInfo> EntityDatas { get; private set; }
        

        protected Project(){ }

        private bool IsDirectory(string filePath)
        {
            return Path.GetFileName(filePath) == Path.GetFileNameWithoutExtension(filePath);
        }

        private string ConstructPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return "";

            if (IsDirectory(filePath))
                return Path.Combine(filePath, $"{Name}.typeo");
            return filePath;
        }

        public bool ScanForSolution()
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
        }

        public void ConstructSolutionPath()
        {
            SolutionFilePath = $@".\{Name}.sln";
        }

        public List<string> FetchProjectsFromSolution()
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
        }

        public bool Save(string filePath = null)
        {
            if(!string.IsNullOrEmpty(filePath))
                ProjectFilePath = ConstructPath(filePath);

            JSON.Serialize(new { 
                Name = Name,
                SolutionFilePath = SolutionFilePath,
                ProjectDLLPath = ProjectDLLPath,
                ProjectName = ProjectName
            }, ProjectFilePath);

            return true;
        }

        public bool Build()
        {
            if (string.IsNullOrEmpty(ProjectFilePath)) return false;
            if (string.IsNullOrEmpty(SolutionFilePath)) return false;

            var path = Path.Combine(ProjectDirPath, SolutionFilePath);
            if (!File.Exists(path)) return false;

            CMD.Run($"dotnet build {path}");

            return true;
        }

        public bool LoadAssembly()
        {
            if (string.IsNullOrEmpty(ProjectFilePath)) return false;
            if (string.IsNullOrEmpty(ProjectDLLPath)) return false;

            var path = Path.Combine(ProjectDirPath, ProjectDLLPath);
            if (!File.Exists(path)) return false;

            Assembly = Assembly.LoadFrom(path);

            return true;
        }

        public bool LoadTypes()
        {
            if (Assembly == null) return false;

            Game = LoadType<Game>().FirstOrDefault();
            Scenes = LoadType<Scene>();
            Entities = LoadType<Entity>();
            Stubs = LoadType<Stub>();
            Logics = LoadType<Logic>();
            Drawables = LoadType<Drawable>();
            EntityDatas = LoadType<EntityData>();

            return true;
        }

        private List<TypeInfo> LoadType<T>()
        {
            var types = Assembly.DefinedTypes;
            return types.Where(
                        t =>
                        t.IsSubclassOf(typeof(T))
                    ).ToList();
        }

        public bool CreateSolution()
        {
            CMD.Run(new string[] {
                $"cd \"{ProjectDirPath}\"",
                $"dotnet new sln --name \"{Path.GetFileNameWithoutExtension(SolutionFilePath)}\""
            });
            return true;
        }

        public bool CreateProject()
        {
            CMD.Run(new string[]
            {
                $"cd \"{ProjectDirPath}\"",
                $"dotnet new console -lang \"C#\" -n \"{ProjectName}\"",
                $"dotnet sln \"{Path.GetFileName(SolutionFilePath)}\" add \"{ProjectName}\""
            });
            return true;
        }
    }
}
