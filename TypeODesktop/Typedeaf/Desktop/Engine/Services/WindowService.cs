using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Desktop.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Contents;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Services
    {
        public class WindowService : Service, IHasGame, IWindowService
        {
            public ILogger Logger { get; set; }
            public IWindowHardware WindowHardware { get; set; }

            public Game Game { get; set; }

            public override void Initialize() { }

            public DesktopWindow CreateWindow()
            {
                Logger.Log($"Createing Window");
                var window = WindowHardware.CreateWindow();
                window.Game = Game;
                (window as IHasContext)?.SetContext(Context);
                Context.SetLogger(window);

                return window;
            }

            public DesktopWindow CreateWindow(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
            {
                var window = CreateWindow();

                window.Initialize(title, position, size, fullscreen, borderless);

                return window;
            }

            public Canvas CreateCanvas(Window window)
            {
                var canvas = WindowHardware.CreateCanvas(window);

                (canvas as IHasContext)?.SetContext(Context);
                Context.SetLogger(canvas);
                canvas.Initialize();

                return canvas;
            }

            public Canvas CreateCanvas(Window window, Rectangle viewport)
            {
                var canvas = CreateCanvas(window);
                canvas.Viewport = viewport;
                return canvas;
            }

            public ContentLoader CreateContentLoader(Canvas canvas)
            {
                var contentLoader = WindowHardware.CreateContentLoader(canvas);

                (contentLoader as IHasContext)?.SetContext(Context);
                Context.SetLogger(contentLoader);

                return contentLoader;
            }
        }
    }
}
