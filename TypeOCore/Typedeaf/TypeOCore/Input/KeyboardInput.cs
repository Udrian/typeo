using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    namespace Input
    {
        public partial class KeyboardInput
        {
            public abstract class Internal
            {
                public abstract bool CurrentKeyDownEvent(object key);
                public abstract bool CurrentKeyUpEvent(object key);
                public abstract bool OldKeyDownEvent(object key);
                public abstract bool OldKeyUpEvent(object key);
            }

            protected Game Game { get; private set; }
            protected TypeO TypeO { get { return (Game as IHasTypeO).GetTypeO(); } }

            public KeyboardInput(Game game)
            {
                Game = game;
            }

            public void SetKeyAlias(object input, object key)
            {
                TypeO.KeyConverter.SetKeyAlias(input, key);
            }

            public bool IsDown(object input)
            {
                if (!TypeO.KeyConverter.ContainsInput(input)) return false;

                return TypeO.KeyHandler.CurrentKeyDownEvent(TypeO.KeyConverter.GetKey(input));
            }
            public bool IsPressed(object input)
            {
                if (!TypeO.KeyConverter.ContainsInput(input)) return false;

                return TypeO.KeyHandler.CurrentKeyDownEvent(TypeO.KeyConverter.GetKey(input)) && TypeO.KeyHandler.OldKeyUpEvent(TypeO.KeyConverter.GetKey(input));
            }
            public bool IsReleased(object input)
            {
                if (!TypeO.KeyConverter.ContainsInput(input)) return false;

                return TypeO.KeyHandler.CurrentKeyUpEvent(TypeO.KeyConverter.GetKey(input)) && TypeO.KeyHandler.OldKeyDownEvent(TypeO.KeyConverter.GetKey(input));
            }
        }

        public partial class InputHandler
        {
            public KeyboardInput Key { get; private set; }
        }
    }

    public partial class TypeO
    {
        public KeyboardInput.Internal KeyHandler { get; set; }
    }
}
