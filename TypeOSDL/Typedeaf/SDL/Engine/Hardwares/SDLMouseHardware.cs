using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.SDL.Engine.Hardwares.Interfaces;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Hardwares
    {
        public class SDLMouseHardware : Hardware, IMouseHardware, ISDLEvents
        {
            public override void Initialize()
            {
            }

            public override void Cleanup()
            {
            }

            public void UpdateEvents(List<SDL2.SDL.SDL_Event> events)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
