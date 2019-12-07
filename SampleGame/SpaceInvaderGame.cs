using Typedeaf.TypeOCommon;
using Typedeaf.TypeOSDL;
using SDL2;
using Typedeaf.TypeOSDL.Content;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Graphics;

namespace SampleGameCore
{
    public class SpaceInvaderGame : SDLGame, Game.Interfaces.ISingleCanvasGame<SDLWindow, SDLCanvas>
    {
        public Vec2 ScreenSize { get; set; } = new Vec2(640, 480);
        public PlayScene Scene { get; set; }
        public SDLContentLoader ContentLoader { get; set; }
        public SDLWindow Window { get; set; }
        public SDLCanvas Canvas { get; set; }

        public override void Initialize()
        {
            CreateWindow("Space Invader", new Vec2(100, 100), ScreenSize);
            Window.CreateCanvas();
            ContentLoader = CreateContentLoader<SDLContentLoader>("", Canvas);
            Scene = Canvas.SetScene<PlayScene>();

            Input.Key.SetKeyAlias("Quit", SDL.SDL_Keycode.SDLK_7);
            Input.Key.SetKeyAlias("Left", SDL.SDL_Keycode.SDLK_LEFT);
            Input.Key.SetKeyAlias("Right", SDL.SDL_Keycode.SDLK_RIGHT);
        }

        public override void Draw()
        {
            Scene.Canvas.Clear(Color.SoftBlack);

            Scene.Draw();

            Scene.Canvas.Present();
        }

        public override void Update(float dt)
        {
            Scene.Update(dt);
        }
    }
}
