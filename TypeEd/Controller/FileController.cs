using System.IO;
using TypeEd.Model;

namespace TypeEd.Controller
{
    public class FileController
    {
        public static Project LoadedProject { get; set;}

        public void Create(string name, string location, string solutionName, string csProjName)
        {
            if (Path.GetFileNameWithoutExtension(location) != name)
            {
                location = Path.Combine(location, name);
            }
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
            }

            LoadedProject = Project.Create(name, location, solutionName, csProjName);
            LoadedProject.Save();
            LoadedProject.CreateSolution();
            LoadedProject.CreateProject();
        }

        public Project Open(string projectFilePath)
        {
            if (!projectFilePath.EndsWith(".typeo")) return null;

            LoadedProject = Project.Load(projectFilePath);
            if (LoadedProject == null) return null;
            LoadedProject.Build();
            LoadedProject.LoadAssembly();
            LoadedProject.LoadTypes();

            return LoadedProject;
        }

        public void Save()
        {
            if (LoadedProject == null) return;
            LoadedProject.Save();
        }
    }
}
