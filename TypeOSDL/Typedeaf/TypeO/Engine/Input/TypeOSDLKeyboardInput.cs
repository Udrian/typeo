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

            public override bool IsDown(Enum input, Enum modifier = null)
            {
                throw new NotImplementedException();
            }

            public override bool IsPressed(Enum input, Enum modifier = null)
            {
                throw new NotImplementedException();
            }

            public override bool IsReleased(Enum input, Enum modifier = null)
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
