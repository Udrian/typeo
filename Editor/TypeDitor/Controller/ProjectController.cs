using System.Threading.Tasks;
using TypeD;
using TypeD.Data;
using TypeD.Models;
using TypeDCore.Commands.Project;
using TypeDCore.Commands.Scene;
using TypeDCore.Commands.Entity;

namespace TypeDEditor.Controller
{
    public static class ProjectController
    {
        public static ProjectModel LoadedProject { get; set; }

        public static void CreateEntity(string className, string @namespace, bool updatable, bool drawable)
        {
            if (LoadedProject == null)
                return;

            Command.Project.CreateEntity(LoadedProject, className, @namespace, updatable, drawable);
        }

        public static void CreateScene(string className, string @namespace)
        {
            if (LoadedProject == null)
                return;

            Command.Project.CreateScene(LoadedProject, className, @namespace);
        }

        public static void CreateDrawable2d(string className, string @namespace)
        {
            if (LoadedProject == null)
                return;

            Command.Project.CreateDrawable2d(LoadedProject, className, @namespace);
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
            Command.Game.SetStartScene(LoadedProject, typeDType);
        }

        public static void AddEntity(TypeDType baseType, TypeDType childEntity)
        {
            Command.Scene.AddEntity(baseType, childEntity);
        }

        public static void AddDrawable2d(TypeDType baseType, TypeDType childDrawable2d)
        {
            Command.Entity.AddDrawable2d(baseType, childDrawable2d);
        }
    }
}
