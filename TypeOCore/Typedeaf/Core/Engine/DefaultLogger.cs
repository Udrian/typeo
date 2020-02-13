using System.Diagnostics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public class DefaultLogger : ILogger
        {
            public LogLevel LogLevel { get; set; }

            public void Log(LogLevel level, string log)
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

                Debug.WriteLine($"{levelMessage}: {log}");
            }
        }
    }
}
