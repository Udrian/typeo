using SDL2;
using System.Collections.Generic;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Input;
using Typedeaf.TypeOCore.Services;

namespace Typedeaf.TypeOSDL
{
    namespace Services
    {
        public class SDLKeyboardInputService : Service, IKeyboardInputService, IIsUpdatable
        {
            private KeyConverter KeyConverter { get; set; }

            private List<SDL.SDL_Event> OldEvents { get; set; }
            private List<SDL.SDL_Event> NewEvents { get; set; }

            public SDLKeyboardInputService()
            {
                KeyConverter = new KeyConverter();

                OldEvents = new List<SDL.SDL_Event>();
                NewEvents = new List<SDL.SDL_Event>();
            }

            public override void Initialize() {}

            public void Update(float dt)
            {
                var es = new List<SDL.SDL_Event>();
                while (SDL.SDL_PollEvent(out SDL.SDL_Event e) > 0)
                {
                    if (e.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        TypeO.Exit();
                    }
                    else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN || e.type == SDL.SDL_EventType.SDL_KEYUP)
                    {
                        es.Add(e);
                    }
                }

                OldEvents = NewEvents;
                NewEvents = es;
            }

            protected bool CurrentKeyDownEvent(object key)
            {
                foreach (var e in NewEvents)
                {
                    if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                    {
                        if (e.key.keysym.sym == (SDL.SDL_Keycode)key) return true;
                    }
                }
                return false;
            }

            protected bool CurrentKeyUpEvent(object key)
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

            protected bool OldKeyDownEvent(object key)
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

            protected bool OldKeyUpEvent(object key)
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
