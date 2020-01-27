using System;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Engine.Hardware.Interfaces;
using Typedeaf.TypeOSDL;
using Typedeaf.TypeOSDL.Engine.Hardware;

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
                .AddHardware<IWindowHardware, SDLWindowHardware>()
                .Start();
        }
    }
}
