using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL;
using SDL2;

namespace SampleGameCore
{
    public class SampleGame : Game
    {
        public Vec2 ScreenSize { get; set; } = new Vec2(640, 480);
        public SampleScene Scene { get; set; }

        public override void Initialize()
        {
            var win = this.CreateWindow("Hello World", new Vec2(100, 100), ScreenSize);
            var canvas =  win.CreateCanvas();
            Scene = canvas.SetScene<SampleScene>();

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
