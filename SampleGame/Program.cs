using System;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL;

namespace SampleGame
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            TypeO.Create<SpaceInvaderGame>()
                .LoadModule<TypeOSDLModule>()
                .AddModuleServices()
                .Start();
        }
    }
}