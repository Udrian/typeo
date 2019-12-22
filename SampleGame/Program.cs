using SampleGameCore;
using System;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL;
using TypeOCore.Typedeaf.TypeOCore.Services;
using TypeOSDL.Typedeaf.TypeOSDL.Services;

namespace SampleGameSDLWin
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            TypeO.Create<SampleGame>()
                .LoadModule<TypeOSDLModule>()
                .AddService<IWindowService, SDLWindowService>()
                .Start();
        }
    }
}
