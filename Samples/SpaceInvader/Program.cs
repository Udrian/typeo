using System;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.SDL;
using TypeOEngine.Typedeaf.SDL.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;

namespace SpaceInvader
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            TypeO.Create<SpaceInvaderGame>()
                .LoadModule<SDLModule>()
                .AddModuleServices()
                .AddHardware<IWindowHardware, SDLWindowHardware>()
                .AddHardware<IKeyboardHardware, SDLKeyboardHardware>()
                .BindContent<Texture, SDLTexture>()
                .BindContent<Font, SDLFont>()
                .Start();
        }
    }
}
