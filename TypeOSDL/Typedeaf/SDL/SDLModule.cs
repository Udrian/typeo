using SDL2;
using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;
using TypeOEngine.Typedeaf.SDL.Engine.Hardwares;
using TypeOEngine.Typedeaf.SDL.Engine.Hardwares.Interfaces;

namespace TypeOEngine.Typedeaf.SDL
{
    public class SDLModule : Module<SDLModuleOption>, IIsUpdatable
    {
        public IKeyboardHardware KeyboardHardware { get; set; }
        public ILogger Logger { get; set; }
        public bool Pause { get; set; }

        public SDLModule() : base(new Core.Engine.Version(0, 1, 0))
        {
        }

        public override void Initialize()
        {
            TypeO.RequireTypeO(new Core.Engine.Version(0, 1, 0));
            TypeO.RequireModule<DesktopModule>(new Core.Engine.Version(0, 1, 0));

            //Initial SDL
            SDL2.SDL.SDL_SetHint(SDL2.SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
            if (SDL2.SDL.SDL_Init(SDL2.SDL.SDL_INIT_VIDEO) != 0)
            {
                var message = $"SDL_Init Error: {SDL2.SDL.SDL_GetError()}";
                Logger.Log(LogLevel.Fatal, message);
                TypeO.Context.Exit();
                throw new ApplicationException(message);
            }

            if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
            {
                var message = $"IMG_Init Error: {SDL2.SDL.SDL_GetError()}";
                Logger.Log(LogLevel.Fatal, message);
                TypeO.Context.Exit();
                throw new ApplicationException(message);
            }

            if(SDL_ttf.TTF_Init() != 0)
            {
                var message = $"TTF_Init Error: {SDL2.SDL.SDL_GetError()}";
                Logger.Log(LogLevel.Fatal, message);
                TypeO.Context.Exit();
                throw new ApplicationException(message);
            }
        }

        public override void Cleanup()
        {
            SDL_ttf.TTF_Quit();
            SDL_image.IMG_Quit();
            SDL2.SDL.SDL_Quit();
        }

        public void Update(double dt)
        {
            var es = new List<SDL2.SDL.SDL_Event>();
            while (SDL2.SDL.SDL_PollEvent(out SDL2.SDL.SDL_Event e) > 0)
            {
                if (e.type == SDL2.SDL.SDL_EventType.SDL_QUIT)
                {
                    TypeO.Context.Exit();
                }
                else if (e.type == SDL2.SDL.SDL_EventType.SDL_KEYDOWN || e.type == SDL2.SDL.SDL_EventType.SDL_KEYUP)
                {
                    es.Add(e);
                }
            }

            if (KeyboardHardware is ISDLKeyboardHardware sdlKeyboardHardware)
            {
                sdlKeyboardHardware.UpdateKeys(es);
            }
        }

        public override void LoadExtensions()
        {
            TypeO.AddService<IWindowService, WindowService>();
            TypeO.AddService<IKeyboardInputService, KeyboardInputService>();
            TypeO.AddHardware<IWindowHardware, SDLWindowHardware>();
            TypeO.AddHardware<IKeyboardHardware, SDLKeyboardHardware>();
            TypeO.BindContent<Texture, SDLTexture>();
            TypeO.BindContent<Font, SDLFont>();
        }
    }
}