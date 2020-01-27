using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Engine.Hardwares.Interfaces
    {
        public interface IWindowHardware : IHardware
        {
            public Window CreateWindow();
        }
    }
}
