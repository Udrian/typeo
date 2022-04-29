namespace TypeDCore.Commands.Data
{
    internal class OpenInExternalCommandData
    {
        public enum CommandAction
        {
            OpenInFolder,
            OpenInEditor
        }

        public string FilePath { get; set; }
        public CommandAction Action { get; set; }

        public OpenInExternalCommandData(string filePath, CommandAction action)
        {
            FilePath = filePath;
            Action = action;
        }
    }
}
