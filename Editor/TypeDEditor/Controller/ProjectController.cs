using System.Threading.Tasks;
using TypeD.Commands.Project;

namespace TypeDEditor.Controller
{
    public static class ProjectController
    {
        public static void AddNewEntity(string className, string @namespace, bool updatable, bool drawable)
        {
            if (FileController.LoadedProject == null)
                return;

            ProjectCommand.AddEntity(FileController.LoadedProject, className, @namespace, updatable, drawable);
        }

        public static async Task Run()
        {
            await FileController.LoadedProject.Build();
            FileController.LoadedProject.Run();
        }

        public static async Task Build()
        {
            await FileController.Save();
            await FileController.LoadedProject.Build();
        }
    }
}
