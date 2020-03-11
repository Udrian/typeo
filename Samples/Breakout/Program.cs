using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Desktop;
using TypeOEngine.Typedeaf.SDL;

namespace Breakout
{
    class Program
    {
        static void Main()
        {
            TypeO.Create<BreakoutGame>("Breakout")
                .LoadModule<DesktopModule>(new DesktopModuleOption() { SaveLogsToDisk = false })
                .LoadModule<SDLModule>()
                .SetLogger(LogLevel.Info)
                .Start();
        }
    }
}
