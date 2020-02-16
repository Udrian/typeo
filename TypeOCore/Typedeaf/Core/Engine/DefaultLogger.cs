using System;
using System.Collections.Generic;
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

            private bool _logToDisk;
            public bool LogToDisk {
                get {
                    return _logToDisk;
                }
                set {
                    _logToDisk = value;
                    if (_logToDisk)
                    {
                        foreach (var log in Logs)
                        {
                            WriteToDisk(log);
                        }
                    }
                }
            }
            public string LogPath { get; set; }
            public LogLevel LogLevel { get; set; }

            private List<string> Logs { get; set; }

            public DefaultLogger()
            {
                Logs = new List<string>();
            }

            public void Log(LogLevel level, string log)
            {
                if (LogLevel == LogLevel.None) return;
                if (LogLevel > level) return;

                var defaultColor = Console.ForegroundColor;
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
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogLevel.Error:
                        levelMessage = "EROR";
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case LogLevel.Fatal:
                        levelMessage = "FATL";
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        break;
                }

                var logMessage = $"{Context.Name} - {levelMessage}: {log}";
                Debug.WriteLine(logMessage);
                Console.WriteLine(logMessage);
                Logs.Add(logMessage);

                Console.ForegroundColor = defaultColor;

                if (LogToDisk)
                {
                    WriteToDisk(logMessage);
                }
            }

            private async void WriteToDisk(string logMessage)
            {
                var dirPath = string.IsNullOrEmpty(LogPath) ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TypeO", Context.Name, "Logs") : LogPath;
                var filePath = $"{DateTime.UtcNow.ToShortDateString()}.log".Replace("/", "-");
                var logPath = Path.Combine(dirPath, filePath);

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                using var logFile = File.AppendText(logPath);
                await logFile.WriteLineAsync(logMessage);
            }
        }
    }
}
