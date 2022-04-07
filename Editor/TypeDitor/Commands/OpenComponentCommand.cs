using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeDitor.Commands.Data;
using TypeDitor.ViewModel;
using TypeDSDL.View.Documents;

namespace TypeDitor.Commands
{
    class OpenComponentCommand : ProjectCommands
    {
        // ViewModel
        MainWindowViewModel MainWindowViewModel { get; set; } //TODO: NOT LIKE THIS

        // Models
        IHookModel HookModel { get; set; }

        public OpenComponentCommand(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel.MainWindow)
        {
            MainWindowViewModel = mainWindowViewModel;
            HookModel = ResourceModel.Get<IHookModel>();
        }

        public override void Execute(object parameter)
        {
            var data = parameter as OpenComponentCommandData;

            //TODO: Check if Component is already open and only shoot if it isn't
            HookModel.Shoot(new OpenComponentHook() { Project = data.Project, Component = data.Component });
            HookModel.Shoot(new ComponentFocusHook() { Project = data.Project, Component = data.Component });

            //TODO: Fix this, TypeDitor shouldnt have access to SDL
            //MainWindowViewModel.OpenDocument($"{data.Component.ClassName}.{data.Component.TypeOBaseType}", new SDLViewerDocument(data.Project, data.Component));
            //MainWindowViewModel.OpenDocument($"{data.Component.ClassName}.{data.Component.TypeOBaseType}", new ConsoleViewerDocument(data.Project, data.Component));
        }
    }
}
