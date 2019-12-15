using Typedeaf.TypeOCommon;
using SDL2;
using Typedeaf.TypeOSDL.Graphics;
using TypeOSDL.Typedeaf.TypeOSDL.Services;
using Typedeaf.TypeOCore;

namespace SampleGameCore
{
    public class SampleGame : Game
    {
        public Vec2 ScreenSize { get; set; } = new Vec2(640, 480);
        public SampleScene Scene { get; set; }
        public SDLWindow Window { get; set; }

        public override void Initialize()
        {
            Window = GetService<SDLWindowService>().CreateWindow("Hello World", new Vec2(100, 100), ScreenSize);
            Scene = Window.SetScene<SampleScene>();

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
