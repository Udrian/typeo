using Typedeaf.TypeOCore.Engine.Hardware;
using Typedeaf.TypeOCore.Engine.Hardware.Interfaces;
using Typedeaf.TypeOCore.Graphics;
using Typedeaf.TypeOSDL.Graphics;

namespace Typedeaf.TypeOSDL
{
    namespace Engine.Hardware
    {
        public class SDLWindowHardware : HardwareBase, IWindowHardware
        {
            public override void Initialize() { }

            Window IWindowHardware.CreateWindow()
            {
                return CreateWindow();
            }

            public SDLWindow CreateWindow()
            {
                return new SDLWindow();
            }
        }
    }
}
