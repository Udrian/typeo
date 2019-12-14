using SDL2;
using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Input;

namespace Typedeaf.TypeOSDL
{
    public partial class TypeOSDLModule : Module
    {
        public override void Initialize()
        {
            //Set keyboard handler through SDL
            TypeO.KeyHandler = SDLKeyboardInput = new SDLKeyboardInputHandler();

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

            SDL_ttf.TTF_Init();
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
                    TypeO.Exit();
                }
                else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN || e.type == SDL.SDL_EventType.SDL_KEYUP)
                {
                    es.Add(e);
                }
            }
            SDLKeyboardInput.Update(es);
        }
    }
}