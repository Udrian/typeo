using System.Diagnostics;
using System.IO;
using TypeD.Commands;
using TypeDCore.Commands.Data;

namespace TypeDCore.Commands
{
    internal class OpenInExternalCommand : CustomCommand<OpenInExternalCommandData>
    {
        public override void Execute(OpenInExternalCommandData parameter)
        {
            if (parameter.Action == OpenInExternalCommandData.CommandAction.OpenInFolder)
            {
                if (File.Exists(parameter.FilePath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = $"/select, \"{parameter.FilePath}\"",
                        FileName = "explorer.exe"
                    };

                    Process.Start(startInfo);
                }
                else if(Directory.Exists(parameter.FilePath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = parameter.FilePath,
                        FileName = "explorer.exe"
                    };

                    Process.Start(startInfo);
                }
            }
        }
    }
}
