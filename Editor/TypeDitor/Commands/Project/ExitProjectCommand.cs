using System.Windows;

namespace TypeDitor.Commands.Project
{
    class ExitProjectCommand : ProjectCommands
    {
        public ExitProjectCommand()
        {
        }

        public override void Execute(object param)
        {
            Application.Current.Shutdown();
        }
    }
}
