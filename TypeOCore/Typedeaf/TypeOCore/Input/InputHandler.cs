using System.Collections.Generic;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    namespace Input
    {
        public partial class InputHandler
        {
            protected TypeO TypeO { get; private set; }

            public InputHandler(TypeO typeO)
            {
                TypeO = typeO;
                Key = new KeyboardInput(TypeO);
            }

        }
    }
    public abstract partial class Game
    {
        public InputHandler Input { get; private set; }
    }
}
