using System;
using System.Collections.Generic;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Input;

namespace Typedeaf.TypeO.Engine
{
    namespace Input
    {
        public abstract partial class KeyboardInput
        {
            protected Core.TypeO TypeO { get; private set; }

            public KeyboardInput(Core.TypeO typeO)
            {
                TypeO = typeO;
            }

            public bool IsDown(object input)
            {
                if (!TypeO.KeyConverter.ContainsInput(input)) return false;

                return CurrentKeyDownEvent(TypeO.KeyConverter.GetKey(input));
            }
            public bool IsPressed(object input)
            {
                if (!TypeO.KeyConverter.ContainsInput(input)) return false;

                return CurrentKeyDownEvent(TypeO.KeyConverter.GetKey(input)) && OldKeyUpEvent(TypeO.KeyConverter.GetKey(input));
            }
            public bool IsReleased(object input)
            {
                if (!TypeO.KeyConverter.ContainsInput(input)) return false;

                return CurrentKeyUpEvent(TypeO.KeyConverter.GetKey(input)) && OldKeyDownEvent(TypeO.KeyConverter.GetKey(input));
            }

            public abstract bool CurrentKeyDownEvent(object key);
            public abstract bool CurrentKeyUpEvent(object key);
            public abstract bool OldKeyDownEvent(object key);
            public abstract bool OldKeyUpEvent(object key);
        }

        public partial class InputHandler
        {
            public KeyboardInput Key { get; set; }
        }
    }

    namespace Core
    {
        public partial class TypeO
        {
            public delegate KeyboardInput CreateKeyboardInputDelegate(TypeO typeO);
            public CreateKeyboardInputDelegate CreateKeyboardInput;
        }
    }
}
