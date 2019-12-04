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
            private Dictionary<SDL.SDL_Keycode, bool> OldState { get; set; }
            private Dictionary<SDL.SDL_Keycode, bool> NewState { get; set; }

            public SDLKeyboardInputHandler()
            {
                OldState = new Dictionary<SDL.SDL_Keycode, bool>();
                NewState = new Dictionary<SDL.SDL_Keycode, bool>();
            }

            public void Update(List<SDL.SDL_Event> es)
            {
                OldState = new Dictionary<SDL.SDL_Keycode, bool>(NewState);

                foreach(var e in es)
                {
                    bool? state = null;
                    if(e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
                    {
                        state = true;
                    }
                    else if(e.type == SDL.SDL_EventType.SDL_KEYUP)
                    {
                        state = false;
                    }

                    if (state.HasValue)
                    {
                        if (!NewState.ContainsKey(e.key.keysym.sym))
                        {
                            NewState.Add(e.key.keysym.sym, state.Value);
                        }
                        else
                        {
                            NewState[e.key.keysym.sym] = state.Value;
                        }
                    }
                }
            }

            public override bool CurrentKeyDownEvent(object key)
            {
                if (!NewState.ContainsKey((SDL.SDL_Keycode)key)) return false;
                return NewState[(SDL.SDL_Keycode)key];
            }

            public override bool CurrentKeyUpEvent(object key)
            {
                if (!NewState.ContainsKey((SDL.SDL_Keycode)key)) return true;
                return !NewState[(SDL.SDL_Keycode)key];
            }

            public override bool OldKeyDownEvent(object key)
            {
                if (!OldState.ContainsKey((SDL.SDL_Keycode)key)) return false;
                return OldState[(SDL.SDL_Keycode)key];
            }

            public override bool OldKeyUpEvent(object key)
            {
                if (!OldState.ContainsKey((SDL.SDL_Keycode)key)) return true;
                return !OldState[(SDL.SDL_Keycode)key];
            }
        }
    }

    public partial class TypeOSDLModule : Module
    {
        public SDLKeyboardInputHandler SDLKeyboardInput { get; set; }
    }
}
