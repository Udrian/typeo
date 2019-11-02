using SDL2;
using System.Collections.Generic;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Input;
using Typedeaf.TypeOSDL.Input;

namespace Typedeaf.TypeOSDL
{
    namespace Input
    {
        public class SDLKeyboardInputHandler : KeyboardInput.Internal
        {
            private List<SDL.SDL_Event> OldEvents { get; set; }
            private List<SDL.SDL_Event> NewEvents { get; set; }

            public SDLKeyboardInputHandler()
            {
                OldEvents = new List<SDL.SDL_Event>();
                NewEvents = new List<SDL.SDL_Event>();
            }

            public void Update(List<SDL.SDL_Event> es)
            {
                OldEvents = NewEvents;
                NewEvents = es;
            }

            public override bool CurrentKeyDownEvent(object key)
            {
                foreach(var e in NewEvents)
                {
                    if(e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                    {
                        if (e.key.keysym.sym == (SDL.SDL_Keycode)key) return true;
                    }
                }
                return false;
            }

            public override bool CurrentKeyUpEvent(object key)
            {
                foreach (var e in NewEvents)
                {
                    if (e.type == SDL.SDL_EventType.SDL_KEYUP)
                    {
                        if (e.key.keysym.sym == (SDL.SDL_Keycode)key) return true;
                    }
                }
                return false;
            }

            public override bool OldKeyDownEvent(object key)
            {
                foreach (var e in OldEvents)
                {
                    if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                    {
                        if (e.key.keysym.sym == (SDL.SDL_Keycode)key) return true;
                    }
                }
                return false;
            }

            public override bool OldKeyUpEvent(object key)
            {
                foreach (var e in OldEvents)
                {
                    if (e.type == SDL.SDL_EventType.SDL_KEYUP)
                    {
                        if (e.key.keysym.sym == (SDL.SDL_Keycode)key) return true;
                    }
                }
                return false;
            }
        }
    }

    public partial class TypeOSDLModule : Module
    {
        public SDLKeyboardInputHandler SDLKeyboardInput { get; set; }
    }
}
