using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Engine.Hardwares.Interfaces;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Services.Interfaces
    {
        public interface IWindowService : IService
        {
            public IWindowHardware WindowHardware { get; set; }

            public Window CreateWindow();
            public Window CreateWindow(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false);
        }
    }
}
