using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TypeD.Models.DTO;
using TypeD.TreeNodes;
using TypeD.Types;

namespace TypeD.Models.Data
{
    public class Project
    {
        // Data
        public string ProjectName { get; private set; }
        public string CSSolutionPath { get; private set; }
        public string CSProjName { get; private set; }
        public List<Module> Modules { get; private set; }
        public string StartScene { get; set; }

        public string Location { get; private set; }

        // Paths
        public string ProjectFilePath { get { return $@"{Location}\{ProjectName}.typeo"; } }
        public string ProjectTypeO { get { return Path.Combine(Location, "typeo"); } }
        public string ProjectBuildOutput { get { return Path.Combine(ProjectTypeO, "build", CSProjName); } }

        // Tree
        internal Tree Tree { get; set; }

        // Loaded data
        internal Dictionary<string, TypeOType> TypeOTypes { get; private set; }
        internal Assembly Assembly { get; set; }

        // Constructor
        internal Project(string location, ProjectDTO projectData)
        {
            Location = location;

            TypeOTypes = new Dictionary<string, TypeOType>();

            ProjectName = projectData.ProjectName;
            CSSolutionPath = projectData.CSSolutionPath;
            CSProjName = projectData.CSProjName;
            Modules = projectData.Modules.Select(m => new Module() { Name = m.Name, Version = m.Version }).ToList();
            StartScene = projectData.StartScene;
        }
    }
}
