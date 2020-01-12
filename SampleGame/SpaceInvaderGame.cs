using Typedeaf.TypeOCommon;
using SDL2;
using Typedeaf.TypeOSDL.Graphics;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Services;
using Typedeaf.TypeOCore.Services;

namespace SampleGameCore
{
    public class SpaceInvaderGame : Game
    {
        public SDLWindowService        WindowService        { get; private set; }
        public SDLKeyboardInputService KeyboardInputService { get; private set; }

        public Vec2      ScreenSize { get; set; } = new Vec2(640, 480);
        public PlayScene Scene      { get; set; }
        public SDLWindow Window     { get; set; }

        public override void Initialize()
        {
            WindowService        = GetService<IWindowService>()        as SDLWindowService;
            KeyboardInputService = GetService<IKeyboardInputService>() as SDLKeyboardInputService;

            KeyboardInputService.SetKeyAlias("Quit", SDL.SDL_Keycode.SDLK_7);
            KeyboardInputService.SetKeyAlias("Left", SDL.SDL_Keycode.SDLK_LEFT);
            KeyboardInputService.SetKeyAlias("Right", SDL.SDL_Keycode.SDLK_RIGHT);

            Window = WindowService.CreateWindow("Space Invader", new Vec2(100, 100), ScreenSize);
            Scene  = Window.SetScene<PlayScene>();
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
