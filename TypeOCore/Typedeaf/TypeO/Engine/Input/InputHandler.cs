using System.Collections.Generic;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Input;

namespace Typedeaf.TypeO.Engine
{
    namespace Input
    {
        public partial class InputHandler
        {
            protected Core.TypeO TypeO { get; private set; }

            public InputHandler(Core.TypeO typeO)
            {
                TypeO = typeO;
                Key = TypeO.CreateKeyboardInput?.Invoke(TypeO);
            }

        }
    }

    namespace Core
    {
        public abstract partial class Game
        {
            public InputHandler Input { get; private set; }
        }
    }
}
