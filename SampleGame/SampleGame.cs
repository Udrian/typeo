using Typedeaf.TypeOCommon;
using Typedeaf.TypeOSDL;
using SDL2;
using Typedeaf.TypeOSDL.Content;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Graphics;

namespace SampleGameCore
{
    public class SampleGame : SDLGame, Game.Interfaces.ISingleCanvasGame<SDLWindow, SDLCanvas>
    {
        public Vec2 ScreenSize { get; set; } = new Vec2(640, 480);
        public SampleScene Scene { get; set; }
        public SDLContentLoader ContentLoader { get; set; }
        public SDLWindow Window { get; set; }
        public SDLCanvas Canvas { get; set; }

        public override void Initialize()
        {
            CreateWindow("Hello World", new Vec2(100, 100), ScreenSize);
            Window.CreateCanvas();
            ContentLoader = CreateContentLoader<SDLContentLoader>("", Canvas);
            Scene = Canvas.SetScene<SampleScene>();

            Input.Key.SetKeyAlias("Quit", SDL.SDL_Keycode.SDLK_7);
        }

        public override void Draw()
        {
            Scene.Canvas.Clear(Color.Red);
            Scene.Canvas.DrawLine(new Vec2(10, 10), new Vec2(150, 150), Color.Blue);

            Scene.Draw();

            Scene.Canvas.Present();
        }

        public override void Update(float dt)
        {
            Scene.Update(dt);
        }
    }
}
