using TypeOEngine.Typedeaf.Core.Engine.Graphics;

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
