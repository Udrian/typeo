using SDL2;
using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Input;
using Typedeaf.TypeOSDL.Input;

namespace Typedeaf.TypeOSDL
{
    namespace Input
    {
        public class SDLKeyboardInput : KeyboardInput
        {
            private List<SDL.SDL_Event> OldEvents { get; set; }
            private List<SDL.SDL_Event> NewEvents { get; set; }

            public SDLKeyboardInput(TypeO typeO) : base(typeO)
            {
                OldEvents = new List<SDL.SDL_Event>();
                NewEvents = new List<SDL.SDL_Event>();
            }

            public void Update(List<SDL.SDL_Event> es)
            {
                OldEvents = NewEvents;
                NewEvents = es;
            }

            protected override bool CurrentKeyDownEvent(object key)
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

            protected override bool CurrentKeyUpEvent(object key)
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

            protected override bool OldKeyDownEvent(object key)
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

            protected override bool OldKeyUpEvent(object key)
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
        public SDLKeyboardInput SDLKeyboardInput { get; set; }

        public KeyboardInput CreateKeyboardInput(TypeO typeO)
        {
            SDLKeyboardInput = new SDLKeyboardInput(typeO);

            return SDLKeyboardInput;
        }
    }
}
