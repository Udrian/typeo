using TypeD.Commands;
using TypeDitor.Commands.Data;
using TypeDitor.View.Documents;
using TypeDitor.ViewModel;

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

            MainWindowViewModel.OpenDocument($"{data.TypeOType.ClassName}.{data.TypeOType.TypeOBaseType}", new ConsoleViewerDocument(data.Project, data.TypeOType));
        }
    }
}
