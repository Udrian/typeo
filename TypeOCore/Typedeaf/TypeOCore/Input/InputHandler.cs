using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    namespace Input
    {
        public partial class InputHandler
        {
            protected Game Game { get; private set; }

            public InputHandler(Game game) //TODO: This should take a TypeO instead, not super happy with this current solution
            {
                Game = game;
                Key = new KeyboardInput(Game);
            }

        }
    }
    partial class Game
    {
        public InputHandler Input { get; private set; }
    }
}
