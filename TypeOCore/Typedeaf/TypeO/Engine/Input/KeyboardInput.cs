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

            public abstract bool IsDown(params object[] args);
            public abstract bool IsPressed(params object[] args);
            public abstract bool IsReleased(params object[] args);
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
