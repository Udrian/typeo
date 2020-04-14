using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Desktop;
using TypeOEngine.Typedeaf.SDL;

namespace Particles
{
    class Program
    {
        static void Main()
        {
            TypeO.Create<ParticlesGame>("Space invader")
                 .LoadModule<DesktopModule>(new DesktopModuleOption() { SaveLogsToDisk = false })
                 .LoadModule<SDLModule>()
                 .SetLogger(LogLevel.Info)
                 .Start();
        }
    }
}
