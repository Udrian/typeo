using TypeD.Commands;
using TypeDitor.Commands.Data;
using TypeDitor.ViewModel;
using TypeDSDL.View.Documents;

namespace TypeDitor.Commands
{
    class OpenComponentCommand : CustomCommand
    {
        // ViewModel
        MainWindowViewModel MainWindowViewModel { get; set; }

        public OpenComponentCommand(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
        }

        public override void Execute(object parameter)
        {
            var data = parameter as OpenComponentCommandData;

            //TODO: Fix this, TypeDitor shouldnt have access to SDL
            MainWindowViewModel.OpenDocument($"{data.Component.ClassName}.{data.Component.TypeOBaseType}", new SDLViewerDocument(data.Project, data.Component));
            //MainWindowViewModel.OpenDocument($"{data.Component.ClassName}.{data.Component.TypeOBaseType}", new ConsoleViewerDocument(data.Project, data.Component));
        }
    }
}
