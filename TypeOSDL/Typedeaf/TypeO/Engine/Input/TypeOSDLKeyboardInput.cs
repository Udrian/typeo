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
            public TypeOSDLKeyboardInput(Core.TypeO typeO) : base(typeO)
            {
            }

            public override bool IsKeyDown(Enum input, Enum modifier = null)
            {
                throw new NotImplementedException();
            }

            public override bool IsKeyPressed(Enum input, Enum modifier = null)
            {
                throw new NotImplementedException();
            }

            public override bool IsKeyReleased(Enum input, Enum modifier = null)
            {
                throw new NotImplementedException();
            }
        }
    }

    namespace Modules
    {
        public partial class TypeOSDL : Module
        {
            public KeyboardInput CreateKeyboardInput(Core.TypeO typeO)
            {
                return new TypeOSDLKeyboardInput(typeO);
            }
        }
    }
}
