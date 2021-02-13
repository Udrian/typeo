using System.Threading.Tasks;
using TypeD.Commands.Game;
using TypeD.Commands.Project;
using TypeD.Commands.Scene;
using TypeD.Data;
using TypeD.Models;

namespace TypeDEditor.Controller
{
    public static class ProjectController
    {
        public static ProjectModel LoadedProject { get; set; }

        public static void CreateEntity(string className, string @namespace, bool updatable, bool drawable)
        {
            if (LoadedProject == null)
                return;

            ProjectCommand.CreateEntity(LoadedProject, className, @namespace, updatable, drawable);
        }

        public static void CreateScene(string className, string @namespace)
        {
            if (LoadedProject == null)
                return;

            ProjectCommand.CreateScene(LoadedProject, className, @namespace);
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

        public static void AddEntity(TypeDType baseD, TypeDType childD)
        {
            SceneCommand.AddEntity(baseD, childD);
        }
    }
}
