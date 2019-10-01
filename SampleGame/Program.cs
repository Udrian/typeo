using SampleGameCore;
using SDL2;
using System;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL;

namespace SampleGameSDLWin
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            TypeO.Create<SampleGame>()
                .LoadModule<TypeOSDLModule>()
                .Start();
        }
    }
}
