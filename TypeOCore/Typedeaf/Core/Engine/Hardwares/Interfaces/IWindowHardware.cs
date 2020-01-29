using TypeOEngine.Typedeaf.Core.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Hardwares.Interfaces
    {
        public interface IWindowHardware : IHardware
        {
            public Window CreateWindow();
        }
    }
}
