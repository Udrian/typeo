using SDL2;
using SpaceInvader.Scenes;
using System;
using TypeOEngine.Typedeaf.Basic.Services;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;

namespace SpaceInvader
{
    class SpaceInvaderGame : Game
    {
        ILogger Logger { get; set; }
        WindowService WindowService { get; set; }
        KeyboardInputService KeyboardInputService { get; set; }
        BasicCamera2dService Camera { get; set; }

        public Random Random { get; set; }
        public SceneList Scenes { get; set; }

        public int Score { get; set; } = 0;

        public override void Initialize()
        {
            Logger.Log("Starting up the Game!");

            Random = new Random();

            KeyboardInputService.SetKeyAlias("Quit",  SDL.SDL_Keycode.SDLK_ESCAPE);
            KeyboardInputService.SetKeyAlias("Left",  SDL.SDL_Keycode.SDLK_LEFT);
            KeyboardInputService.SetKeyAlias("Right", SDL.SDL_Keycode.SDLK_RIGHT);
            KeyboardInputService.SetKeyAlias("Up",    SDL.SDL_Keycode.SDLK_UP);
            KeyboardInputService.SetKeyAlias("Down",  SDL.SDL_Keycode.SDLK_DOWN);
            KeyboardInputService.SetKeyAlias("Shoot", SDL.SDL_Keycode.SDLK_z);

            Scenes = CreateSceneHandler();

            Scenes.Window = WindowService.CreateWindow("Space Invader", new Vec2(100, 100), new Vec2(1280, 1024));
            Scenes.Canvas = WindowService.CreateCanvas(Scenes.Window);
            Scenes.ContentLoader = WindowService.CreateContentLoader(Scenes.Canvas);
            Scenes.SetScene<SpaceScene>();

            Camera.Canvas = Scenes.Canvas;
            Camera.Position = new Vec2(138, 5);
        }

        public override void Draw()
        {
            Scenes.Draw();
        }

        public override void Update(double dt)
        {
            Scenes.Update(dt);
        }

        public override void Cleanup()
        {
            Scenes.Cleanup();
        }
    }
}
