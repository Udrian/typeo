using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Engine.Hardware.Interfaces
    {
        public interface IWindowHardware : IHardware
        {
            public Window CreateWindow();
        }
    }
}
