using System.Threading.Tasks;
using TypeD.Commands.Game;
using TypeD.Commands.Project;
using TypeD.Data;
using TypeD.Models;

namespace TypeDEditor.Controller
{
    public static class ProjectController
    {
        public static ProjectModel LoadedProject { get; set; }

        public static void AddNewEntity(string className, string @namespace, bool updatable, bool drawable)
        {
            if (LoadedProject == null)
                return;

            ProjectCommand.AddEntity(LoadedProject, className, @namespace, updatable, drawable);
        }

        public static void AddNewScene(string className, string @namespace)
        {
            if (LoadedProject == null)
                return;

            ProjectCommand.AddScene(LoadedProject, className, @namespace);
        }

        public static async Task Run()
        {
            await Build();
            LoadedProject.Run();
        }

        public static async Task Build()
        {
            await FileController.Save();
            await LoadedProject.Build();
        }

        public static void SetStartScene(TypeDType typeDType)
        {
            GameCommand.SetStartScene(LoadedProject, typeDType);
        }
    }
}
