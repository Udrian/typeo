using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Services.Interfaces
    {
        public interface IWindowService : IService
        {
            public IWindowHardware WindowHardware { get; set; }

            public Window CreateWindow();
            public Window CreateWindow(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false);
        }
    }
}
