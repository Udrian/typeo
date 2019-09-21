using SDL2;
using System;
using System.Collections.Generic;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Graphics;

namespace Typedeaf.TypeO.Engine.Modules
{
    public partial class TypeOSDL : Module
    {
        public TypeOSDL(Core.TypeO typeO) : base(typeO) { }

        public override void Init()
        {
            //Set the delegate to create a window through SDL
            TypeO.CreateWindow += CreateWindow;
            //Set the delegate to create a new Canvas through SDL
            TypeO.CreateCanvas += CreateCanvas;
            //Set keyboard handler through SDL
            TypeO.CreateKeyboardInput += CreateKeyboardInput;

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

        public override void Cleanup()
        {
            //SDL.SDL_DestroyRenderer(ren);
            //SDL.SDL_DestroyWindow(win);
            SDL.SDL_Quit();
        }

        public override void Update(float dt)
        {
            var es = new List<SDL.SDL_Event>();
            while (SDL.SDL_PollEvent(out SDL.SDL_Event e) > 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    TypeO.Game.Exit = true;
                }
                es.Add(e);
            }
            TypeOSDLKeyboardInput.Update(es);
        }
    }
}