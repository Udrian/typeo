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

            public abstract bool IsKeyDown(Enum input, Enum modifier = null);
            public abstract bool IsKeyPressed(Enum input, Enum modifier = null);
            public abstract bool IsKeyReleased(Enum input, Enum modifier = null);
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
