using TypeD.Commands;
using TypeDitor.Commands.Data;
using TypeDitor.View.Documents;
using TypeDitor.ViewModel;
using TypeDSDL.View.Documents;

namespace TypeDitor.Commands
{
    class OpenTypeOTypeCommand : CustomCommand
    {
        // ViewModel
        MainWindowViewModel MainWindowViewModel { get; set; }

        public OpenTypeOTypeCommand(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
        }

        public override void Execute(object parameter)
        {
            var data = parameter as OpenTypeOTypeCommandData;

            //TODO: Fix this, TypeDitor shouldnt have access to SDL
            MainWindowViewModel.OpenDocument($"{data.TypeOType.ClassName}.{data.TypeOType.TypeOBaseType}", new SDLViewerDocument(data.Project, data.TypeOType));
            //MainWindowViewModel.OpenDocument($"{data.TypeOType.ClassName}.{data.TypeOType.TypeOBaseType}", new ConsoleViewerDocument(data.Project, data.TypeOType));
        }
    }
}
