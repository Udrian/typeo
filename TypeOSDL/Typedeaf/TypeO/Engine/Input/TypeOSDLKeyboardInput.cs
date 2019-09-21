using SDL2;
using System;
using System.Collections.Generic;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Input;

namespace Typedeaf.TypeO.Engine
{
    namespace Input
    {
        public class TypeOSDLKeyboardInput : KeyboardInput
        {
            private List<SDL.SDL_Event> OldEvents { get; set; }
            private List<SDL.SDL_Event> NewEvents { get; set; }

            public TypeOSDLKeyboardInput(Core.TypeO typeO) : base(typeO)
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

    namespace Modules
    {
        public partial class TypeOSDL : Module
        {
            public TypeOSDLKeyboardInput TypeOSDLKeyboardInput { get; set; }

            public KeyboardInput CreateKeyboardInput(Core.TypeO typeO)
            {
                TypeOSDLKeyboardInput = new TypeOSDLKeyboardInput(typeO);

                return TypeOSDLKeyboardInput;
            }
        }
    }
}
