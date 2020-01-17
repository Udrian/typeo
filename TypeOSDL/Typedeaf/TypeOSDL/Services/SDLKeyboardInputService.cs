using SDL2;
using System.Collections.Generic;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Input;
using Typedeaf.TypeOCore.Services;

namespace Typedeaf.TypeOSDL
{
    namespace Services
    {
        public class SDLKeyboardInputService : Service, IKeyboardInputService
        {
            private KeyConverter KeyConverter { get; set; }
            
            private Dictionary<SDL.SDL_Keycode, bool> OldState { get; set; }
            private Dictionary<SDL.SDL_Keycode, bool> NewState { get; set; }

            public SDLKeyboardInputService()
            {
                KeyConverter = new KeyConverter();

                OldState = new Dictionary<SDL.SDL_Keycode, bool>();
                NewState = new Dictionary<SDL.SDL_Keycode, bool>();
            }

            public override void Initialize() {}

            public void UpdateKeys(List<SDL.SDL_Event> es)
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

            protected bool CurrentKeyDownEvent(object key)
            {
                if (!NewState.ContainsKey((SDL.SDL_Keycode)key)) return false;
                return NewState[(SDL.SDL_Keycode)key];
            }

            protected bool CurrentKeyUpEvent(object key)
            {
                if (!NewState.ContainsKey((SDL.SDL_Keycode)key)) return true;
                return !NewState[(SDL.SDL_Keycode)key];
            }

            protected bool OldKeyDownEvent(object key)
            {
                if (!OldState.ContainsKey((SDL.SDL_Keycode)key)) return false;
                return OldState[(SDL.SDL_Keycode)key];
            }

            protected bool OldKeyUpEvent(object key)
            {
                if (!OldState.ContainsKey((SDL.SDL_Keycode)key)) return true;
                return !OldState[(SDL.SDL_Keycode)key];
            }

            public void SetKeyAlias(object input, object key)
            {
                KeyConverter.SetKeyAlias(input, key);
            }

            public bool IsDown(object input)
            {
                if (!KeyConverter.ContainsInput(input)) return false;

                return CurrentKeyDownEvent(KeyConverter.GetKey(input));
            }
            public bool IsPressed(object input)
            {
                if (!KeyConverter.ContainsInput(input)) return false;

                return CurrentKeyDownEvent(KeyConverter.GetKey(input)) && OldKeyUpEvent(KeyConverter.GetKey(input));
            }
            public bool IsReleased(object input)
            {
                if (!KeyConverter.ContainsInput(input)) return false;

                return CurrentKeyUpEvent(KeyConverter.GetKey(input)) && OldKeyDownEvent(KeyConverter.GetKey(input));
            }
        }
    }
}
