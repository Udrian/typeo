using SDL2;
using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;
using TypeOEngine.Typedeaf.SDL.Engine.Hardwares;

namespace TypeOEngine.Typedeaf.SDL
{
    public partial class SDLModule : Module, IIsUpdatable
    {
        public IKeyboardHardware KeyboardHardware { get; set; }
        public bool Pause { get; set; }

        public override void Initialize()
        {
            //Initial SDL
            SDL2.SDL.SDL_SetHint(SDL2.SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
            if (SDL2.SDL.SDL_Init(SDL2.SDL.SDL_INIT_VIDEO) != 0)
            {
                Console.WriteLine("SDL_Init Error: " + SDL2.SDL.SDL_GetError());
                return;
            }

            if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
            {
                Console.WriteLine("SDL_Init Error: " + SDL2.SDL.SDL_GetError());
                return;
            }

            SDL_ttf.TTF_Init();
        }

        public override void Cleanup()
        {
            //SDL.SDL_DestroyRenderer(ren);
            //SDL.SDL_DestroyWindow(win);
            SDL2.SDL.SDL_Quit();
        }

        public SDLModule AddDefaultSDLServices()
        {
            TypeO.AddService<IWindowService, WindowService>();
            TypeO.AddService<IKeyboardInputService, KeyboardInputService>();

            return this;
        }
        public SDLModule AddDefaultSDLHardware()
        {
            TypeO.AddHardware<IWindowHardware, SDLWindowHardware>();
            TypeO.AddHardware<IKeyboardHardware, SDLKeyboardHardware>();

            return this;
        }
        public SDLModule AddDefaultSDLContentBinding()
        {
            TypeO.BindContent<Texture, SDLTexture>();
            TypeO.BindContent<Font, SDLFont>();

            return this;
        }

        public void Update(double dt)
        {
            var es = new List<SDL2.SDL.SDL_Event>();
            while (SDL2.SDL.SDL_PollEvent(out SDL2.SDL.SDL_Event e) > 0)
            {
                if (e.type == SDL2.SDL.SDL_EventType.SDL_QUIT)
                {
                    TypeO.Exit();
                }
                else if (e.type == SDL2.SDL.SDL_EventType.SDL_KEYDOWN || e.type == SDL2.SDL.SDL_EventType.SDL_KEYUP)
                {
                    es.Add(e);
                }
            }

            if (KeyboardHardware is SDLKeyboardHardware sdlKeyboardHardware)
            {
                sdlKeyboardHardware.UpdateKeys(es);
            }
        }
    }
}