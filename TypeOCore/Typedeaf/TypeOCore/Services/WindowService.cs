using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Engine.Hardwares.Interfaces;
using Typedeaf.TypeOCore.Graphics;
using Typedeaf.TypeOCore.Interfaces;
using Typedeaf.TypeOCore.Services.Interfaces;

namespace Typedeaf.TypeOCore
{
    namespace Services
    {
        public class WindowService : Service, IHasGame, IWindowService
        {
            public IWindowHardware WindowHardware { get; set; }

            public Game Game { get; set; }

            public override void Initialize() { }

            public Window CreateWindow()
            {
                var window = WindowHardware.CreateWindow();
                window.Game = Game;
                (window as IHasTypeO).SetTypeO(TypeO);
                return window;
            }

            public Window CreateWindow(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
            {
                var window = CreateWindow();
                window.Initialize(title, position, size, fullscreen, borderless);

                return window;
            }
        }
    }
}
