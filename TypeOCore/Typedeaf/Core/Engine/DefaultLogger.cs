using System;
using System.Diagnostics;
using System.IO;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public class DefaultLogger : ILogger, IHasContext
        {
            Context IHasContext.Context { get; set; }
            private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }

            public bool LogToDisk { get; set; }
            public string LogPath { get; set; }
            public LogLevel LogLevel { get; set; }

            public async void Log(LogLevel level, string log)
            {
                if (LogLevel == LogLevel.None) return;
                if (LogLevel > level) return;

                string levelMessage = "";
                switch (level)
                {
                    case LogLevel.Debug:
                        levelMessage = "DBUG";
                        break;
                    case LogLevel.Info:
                        levelMessage = "INFO";
                        break;
                    case LogLevel.Warning:
                        levelMessage = "WARN";
                        break;
                    case LogLevel.Error:
                        levelMessage = "EROR";
                        break;
                    case LogLevel.Fatal:
                        levelMessage = "FATL";
                        break;
                    default:
                        break;
                }

                var logMessage = $"{Context.Name} - {levelMessage}: {log}";
                Debug.WriteLine(logMessage);

                if (LogToDisk)
                {
                    var dirPath = string.IsNullOrEmpty(LogPath) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TypeO", Context.Name, "Logs") : LogPath;
                    var filePath = $"{DateTime.UtcNow.ToShortDateString()}.log".Replace("/", "-");
                    var logPath = Path.Combine(dirPath, filePath);

                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    using (var logFile = File.AppendText(logPath))
                    {
                        await logFile.WriteLineAsync(logMessage);
                    }
                }
            }
        }
    }
}
