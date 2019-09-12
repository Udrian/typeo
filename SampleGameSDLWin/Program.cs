using SampleGameCore;
using System;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Modules;

namespace SampleGameSDLWin
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            TypeO.Create<SampleGame>()
                .LoadModule(new TypeOSDL())
                .Start()
            .Wait();
        }
    }
}
