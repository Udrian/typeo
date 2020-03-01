using System;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Desktop;
using TypeOEngine.Typedeaf.SDL;

namespace SpaceInvader
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            TypeO.Create<SpaceInvaderGame>("Space invader")
                .LoadModule<DesktopModule>(new DesktopModuleOption() { SaveLogsToDisk = false })
                .LoadModule<SDLModule>()
                .SetLogger(LogLevel.Info)
                .Start();
        }
    }
}
