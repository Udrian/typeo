using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Core.Graphics;
using TypeOEngine.Typedeaf.SDL.Graphics;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Hardwares
    {
        public class SDLWindowHardware : Hardware, IWindowHardware
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
