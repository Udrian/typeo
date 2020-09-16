using TypeOEngine.Typedeaf.Basic2d;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Desktop;
using TypeOEngine.Typedeaf.SDL;

namespace SpaceInvader
{
    public static class Program
    {
        public static void Main()
        {
            TypeO.Create<SpaceInvaderGame>("Space invader")
                .LoadModule<DesktopModule>(new DesktopModuleOption() { SaveLogsToDisk = false })
                .LoadModule<SDLModule>()
                .LoadModule<Basic2dModule>()
                .SetLogger(LogLevel.Info)
                .Start();
        }
    }
}
