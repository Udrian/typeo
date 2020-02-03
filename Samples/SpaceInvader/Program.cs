using System;
using TypeOEngine.Typedeaf.SDL;
using TypeOEngine.Typedeaf.Core.Engine;

namespace SpaceInvader
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            TypeO.Create<SpaceInvaderGame>()
                .LoadModule<SDLModule>()
                .AddDefaultSDLServices()
                .AddDefaultSDLHardware()
                .AddDefaultSDLContentBinding()
                .Start();
        }
    }
}
