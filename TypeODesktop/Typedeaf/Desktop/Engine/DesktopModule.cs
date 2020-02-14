using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.SDL
{
    public partial class DesktopModule : Module
    {
        public string LogPath { get; private set; }

        public DesktopModule(TypeO typeO) : base(typeO)
        {
            LogPath = null;
        }

        public override void Cleanup()
        {
        }

        public override void Initialize()
        {
            if(TypeO.Context.Logger is DefaultLogger)
            {
                (TypeO.Context.Logger as DefaultLogger).LogToDisk = true;
                if(!string.IsNullOrEmpty(LogPath))
                {
                    (TypeO.Context.Logger as DefaultLogger).LogPath = LogPath;
                }
            }
            
        }

        public ITypeO SetDefaultLoggerPath(string logPath)
        {
            LogPath = logPath;
            return TypeO;
        }
    }
}