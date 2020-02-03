using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Services
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
