using TypeD.Commands.Project;

namespace TypeDEditor.Controller
{
    public class ProjectController
    {
        public void AddNewEntity(string className, string @namespace)
        {
            if (FileController.LoadedProject == null)
                return;

            ProjectCommand.AddEntity(FileController.LoadedProject, className, @namespace);
        }
    }
}
