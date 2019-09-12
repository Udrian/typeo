using SDL2;
using System;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Graphics;

namespace Typedeaf.TypeO.Engine.Modules
{
    public partial class TypeOSDL : Module
    {
        public override void Init(Core.TypeO typeO)
        {
            //Set the delegate to create a window through SDL
            typeO.CreateWindow += CreateWindow;

            //Initial SDL
            SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                Console.WriteLine("SDL_Init Error: " + SDL.SDL_GetError());
                return;
            }

            if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
            {
                Console.WriteLine("SDL_Init Error: " + SDL.SDL_GetError());
                return;
            }
        }
    }
}