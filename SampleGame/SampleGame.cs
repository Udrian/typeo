using Typedeaf.TypeOCommon;
using SDL2;
using Typedeaf.TypeOSDL.Graphics;
using TypeOSDL.Typedeaf.TypeOSDL.Services;
using Typedeaf.TypeOCore;
using TypeOCore.Typedeaf.TypeOCore.Services;

namespace SampleGameCore
{
    public class SampleGame : Game
    {
        public Vec2 ScreenSize { get; set; } = new Vec2(640, 480);
        public SampleScene Scene { get; set; }
        public SDLWindow Window { get; set; }

        public override void Initialize()
        {
            var windowService = GetService<IWindowService>() as SDLWindowService;
            Window = windowService.CreateWindow("Hello World", new Vec2(100, 100), ScreenSize);
            Scene = Window.SetScene<SampleScene>();

            Input.Key.SetKeyAlias("Quit", SDL.SDL_Keycode.SDLK_7);
        }

        public override void Draw()
        {
            Window.Draw();
        }

        public override void Update(float dt)
        {
            Window.Update(dt);
        }
    }
}
