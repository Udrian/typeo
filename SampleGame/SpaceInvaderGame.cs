using Typedeaf.TypeOCommon;
using SDL2;
using Typedeaf.TypeOSDL.Graphics;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Services;
using Typedeaf.TypeOCore.Services;
using System;

namespace SampleGame
{
    public class SpaceInvaderGame : Game
    {
        public SDLWindowService        WindowService        { get; private set; }
        public SDLKeyboardInputService KeyboardInputService { get; private set; }

        public Vec2      ScreenSize { get; set; } = new Vec2(640, 480);
        public SDLWindow Window     { get; set; }
        public Random    Random     { get; set; }

        public override void Initialize()
        {
            Random = new Random();

            WindowService        = GetService<IWindowService>()        as SDLWindowService;
            KeyboardInputService = GetService<IKeyboardInputService>() as SDLKeyboardInputService;

            KeyboardInputService.SetKeyAlias("Quit", SDL.SDL_Keycode.SDLK_ESCAPE);
            KeyboardInputService.SetKeyAlias("Left", SDL.SDL_Keycode.SDLK_LEFT);
            KeyboardInputService.SetKeyAlias("Right", SDL.SDL_Keycode.SDLK_RIGHT);
            KeyboardInputService.SetKeyAlias("Shoot", SDL.SDL_Keycode.SDLK_UP);

            Window = WindowService.CreateWindow("Space Invader", new Vec2(100, 100), ScreenSize);
            Window.SetScene<PlayScene>();
        }

        public override void Draw()
        {
            Window.Draw();
        }

        public override void Update(double dt)
        {
            Window.Update(dt);
        }
    }
}
