using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace TypeOEditor.Helper
{
    public static class CMD
    {
        private static Process Process { get; set; }
        private static Task ProcessTask { get; set; }
        private static Queue<string> Cmds { get; set; } = new Queue<string>();

        public static void Run(string cmd)
        {
            Run(new string[] { cmd });
        }

        public static void Run(string[] cmds)
        {
            foreach(var cmd in cmds)
            {
                Cmds.Enqueue(cmd);
            }

            if(Process == null || Process.HasExited)
            {
                Process = new Process();
                var info = new ProcessStartInfo();
                info.FileName = "cmd.exe";
                info.RedirectStandardInput = true;
                info.UseShellExecute = false;

                Process.StartInfo = info;
                Process.Start();
            }
            if(ProcessTask == null || ProcessTask.IsCompleted)
            {
                ProcessTask = new Task(() =>
                {
                    using (StreamWriter sw = Process.StandardInput)
                    {
                        while (Cmds.Count > 0)
                        {
                            var cmd = Cmds.Dequeue();
                            if (sw.BaseStream.CanWrite)
                            {
                                sw.WriteLine(cmd);
                            }
                        }
                    }
                });
                ProcessTask.Start();
            }
        }
    }
}
