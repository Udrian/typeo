using TypeOEngine.Typedeaf.Basic.Services;
using TypeOEngine.Typedeaf.Basic.Services.Interfaces;
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
                .AddService<ICamera2dService, BasicCamera2dService>()
                .SetLogger(LogLevel.Info)
                .Start();
        }
    }
}
