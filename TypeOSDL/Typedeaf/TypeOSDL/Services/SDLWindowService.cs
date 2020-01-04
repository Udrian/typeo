using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Graphics;
using Typedeaf.TypeOCore.Services;
using Typedeaf.TypeOSDL.Graphics;

namespace Typedeaf.TypeOSDL
{
    namespace Services
    {
        public class SDLWindowService : Service, IHasGame<Game>, IWindowService
        {
            public Game Game { get; set; }

            public override void Initialize() { }

            Window IWindowService.CreateWindow()
            {
                return CreateWindow();
            }

            public SDLWindow CreateWindow()
            {
                var window = new SDLWindow();
                window.Game = Game;
                (window as IHasTypeO).SetTypeO(TypeO);
                return window;
            }

            public SDLWindow CreateWindow(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
            {
                var window = CreateWindow();
                window.Initialize(title, position, size, fullscreen, borderless);

                return window;
            }
        }
    }
}
