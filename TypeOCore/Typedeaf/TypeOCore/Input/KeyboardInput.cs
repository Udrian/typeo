using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    namespace Input
    {
        public abstract partial class KeyboardInput
        {
            protected TypeO TypeO { get; private set; }

            public KeyboardInput(TypeO typeO)
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

            protected abstract bool CurrentKeyDownEvent(object key);
            protected abstract bool CurrentKeyUpEvent(object key);
            protected abstract bool OldKeyDownEvent(object key);
            protected abstract bool OldKeyUpEvent(object key);
        }

        public partial class InputHandler
        {
            public KeyboardInput Key { get; set; }
        }
    }

    public partial class TypeO
    {
        public delegate KeyboardInput CreateKeyboardInputDelegate(TypeO typeO);
        public CreateKeyboardInputDelegate CreateKeyboardInput;
    }
}
