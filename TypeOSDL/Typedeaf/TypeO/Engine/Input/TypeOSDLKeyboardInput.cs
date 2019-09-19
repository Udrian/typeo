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

            public override bool IsDown(params object[] args)
            {
                return false;
            }

            public override bool IsPressed(params object[] args)
            {
                return false;
            }

            public override bool IsReleased(params object[] args)
            {
                return false;
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
