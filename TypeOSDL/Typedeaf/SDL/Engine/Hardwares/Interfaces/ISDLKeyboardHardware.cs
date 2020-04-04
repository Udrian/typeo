using System.Collections.Generic;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Hardwares.Interfaces
    {
        public interface ISDLKeyboardHardware : IKeyboardHardware
        {
            void UpdateKeys(List<SDL2.SDL.SDL_Event> es);
        }
    }
}
