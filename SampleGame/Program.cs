using System;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Engine.Hardwares.Interfaces;
using Typedeaf.TypeOSDL;
using Typedeaf.TypeOSDL.Engine.Hardwares;

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
                .AddHardware<IKeyboardHardware, SDLKeyboardHardware>()
                .Start();
        }
    }
}
