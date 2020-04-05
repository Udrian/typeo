using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.SDL.Engine.Hardwares.Interfaces;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Hardwares
    {
        public class SDLMouseHardware : Hardware, IMouseHardware, ISDLEvents
        {
            public Vec2 CurrentMousePosition { get; set; }
            public Vec2 OldMousePosition { get; set; }
            public Vec2 CurrentWheelPosition { get; set; }
            public Vec2 OldWheelPosition { get; set; }

            public override void Initialize()
            {
                CurrentMousePosition = new Vec2();
                OldMousePosition = new Vec2();
                CurrentWheelPosition = new Vec2();
                OldWheelPosition = new Vec2();
            }

            public override void Cleanup()
            {
            }

            public void UpdateEvents(List<SDL2.SDL.SDL_Event> events)
            {
                OldMousePosition = new Vec2(CurrentMousePosition);
                OldWheelPosition = new Vec2(CurrentWheelPosition);
                foreach (var e in events)
                {
                    if(e.type == SDL2.SDL.SDL_EventType.SDL_MOUSEWHEEL)
                    {
                        CurrentWheelPosition += new Vec2(e.wheel.x, e.wheel.y);
                    }
                    else if(e.type == SDL2.SDL.SDL_EventType.SDL_MOUSEMOTION)
                    {
                        CurrentMousePosition = new Vec2(e.motion.x, e.motion.y);
                    }
                    else if(e.type == SDL2.SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
                    {
                    }
                }
            }

            public bool CurrentButtonDownEvent(object key)
            {
                throw new System.NotImplementedException();
            }

            public bool CurrentButtonUpEvent(object key)
            {
                throw new System.NotImplementedException();
            }

            public bool OldButtonDownEvent(object key)
            {
                throw new System.NotImplementedException();
            }

            public bool OldButtonUpEvent(object key)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
