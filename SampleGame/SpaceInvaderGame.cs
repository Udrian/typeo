using Typedeaf.TypeOCommon;
using SDL2;
using Typedeaf.TypeOCore;
using System;
using Typedeaf.TypeOCore.Services.Interfaces;
using Typedeaf.TypeOCore.Graphics;

namespace SampleGame
{
    public class SpaceInvaderGame : Game
    {
        public IWindowService WindowService { get; private set; }
        public IKeyboardInputService KeyboardInputService { get; private set; }

        public Vec2   ScreenSize { get; set; } = new Vec2(640, 480);
        public Window Window     { get; set; }
        public Random Random     { get; set; }

        public override void Initialize()
        {
            Random = new Random();

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
