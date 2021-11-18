using System.Windows;

namespace TypeDitor.Commands
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
