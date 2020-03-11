using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Services.Interfaces
    {
        public interface IWindowService : IService
        {
            public DesktopWindow CreateWindow();
            public DesktopWindow CreateWindow(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false);

            public Canvas CreateCanvas(Window window);
            public Canvas CreateCanvas(Window window, Rectangle viewport);
            public ContentLoader CreateContentLoader(Canvas canvas);
        }
    }
}
