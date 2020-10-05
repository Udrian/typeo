using System.Diagnostics;
using System.IO;

namespace TypeOEditor.Helper
{
    public static class CMD
    {
        public static void Run(string cmd)
        {
            Run(new string[] { cmd });
        }

        public static void Run(string[] cmds)
        {
            using (var p = new Process())
            {
                var info = new ProcessStartInfo();
                info.FileName = "cmd.exe";
                info.RedirectStandardInput = true;
                info.UseShellExecute = false;

                p.StartInfo = info;
                p.Start();

                using (StreamWriter sw = p.StandardInput)
                {
                    foreach (var cmd in cmds)
                    {
                        if (sw.BaseStream.CanWrite)
                        {
                            sw.WriteLine(cmd);
                        }
                    }
                }
            }
        }
    }
}
