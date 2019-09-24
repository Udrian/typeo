using SampleGameCore;
using SDL2;
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
                .LoadModule<TypeOSDL>()
                .SetKey("Quit", SDL.SDL_Keycode.SDLK_7)
                .Start();
        }
    }
}
