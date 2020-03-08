using SDL2;
using System;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace Breakout
{
    class BreakoutGame : Game
    {
        public IWindowService WindowService { get; set; }
        public IKeyboardInputService KeyboardInputService { get; set; }

        public Vec2 ScreenSize { get; set; } = new Vec2(1280, 1024);
        public SceneList Scenes { get; set; }

        public override void Initialize()
        {
            KeyboardInputService.SetKeyAlias("Quit", SDL.SDL_Keycode.SDLK_ESCAPE);
            KeyboardInputService.SetKeyAlias("Left", SDL.SDL_Keycode.SDLK_LEFT);
            KeyboardInputService.SetKeyAlias("Right", SDL.SDL_Keycode.SDLK_RIGHT);

            Scenes = CreateSceneHandler();

            Scenes.Window = WindowService.CreateWindow("Brakeout", new Vec2(100, 100), ScreenSize);
            Scenes.Canvas = WindowService.CreateCanvas(Scenes.Window);
            Scenes.ContentLoader = WindowService.CreateContentLoader(Scenes.Canvas);
            Scenes.SetScene<GameScene>();
        }

        public override void Cleanup()
        {
            Scenes.Cleanup();
        }

        public override void Update(double dt)
        {
            Scenes.Update(dt);
        }

        public override void Draw()
        {
            Scenes.Canvas.Clear(Color.Black);
            Scenes.Draw();
            Scenes.Canvas.Present();
        }
    }
}
