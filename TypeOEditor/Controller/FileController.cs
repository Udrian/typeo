using TypeOEditor.Model;

namespace TypeOEditor.Controller
{
    public class FileController
    {
        public static Project LoadedProject { get; set;}

        public Project Create(string name, string filePath)
        {
            var project = Project.Create(name, filePath);

            if(!project.ScanForSolution())
            {
                project.ConstructSolutionPath();
            }

            return project;
        }

        public void Create(Project project)
        {
            project.CreateSolution();
            project.CreateProject();
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
