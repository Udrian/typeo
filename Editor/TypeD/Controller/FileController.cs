using System.IO;
using System.Threading.Tasks;
using TypeD.Model;

namespace TypeD.Controller
{
    public class FileController
    {
        public static Project LoadedProject { get; set;}

        public async Task Create(string name, string location, string solutionName, string csProjName)
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
            await LoadedProject.CreateSolution();
            await LoadedProject.CreateProject();

            LoadedProject.GenerateProjectFiles();

            LoadedProject.AddModule(new Module("TypeOCore"));

            await BuildAndLoadAssembly(LoadedProject);
        }

        public async Task<Project> Open(string projectFilePath)
        {
            if (!projectFilePath.EndsWith(".typeo")) return null;

            LoadedProject = Project.Load(projectFilePath);
            if (LoadedProject == null) return null;

            await BuildAndLoadAssembly(LoadedProject);

            return LoadedProject;
        }

        private async Task BuildAndLoadAssembly(Project project)
        {
            var loaded = true;
            if (!LoadedProject.LoadAssembly())
            {
                await LoadedProject.Build();
                loaded = LoadedProject.LoadAssembly();
            }
            if(loaded)
            {
                LoadedProject.LoadTypes();
            }
        }

        public void Save()
        {
            if (LoadedProject == null) return;
            LoadedProject.Save();
        }
    }
}
