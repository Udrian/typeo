using Typedeaf.TypeOCore.Engine.Hardwares.Interfaces;
using Typedeaf.TypeOCore.Input;
using Typedeaf.TypeOCore.Services.Interfaces;

namespace Typedeaf.TypeOCore
{
    namespace Services
    {
        public class KeyboardInputService : Service, IHasGame<Game>, IKeyboardInputService
        {
            public IKeyboardHardware KeyboardHardware { get; set; }

            public Game Game { get; set; }
            protected KeyConverter KeyConverter { get; set; }

            public override void Initialize()
            {
                KeyConverter = new KeyConverter();
            }

            public void SetKeyAlias(object input, object key)
            {
                KeyConverter.SetKeyAlias(input, key);
            }

            public bool IsDown(object input)
            {
                if (!KeyConverter.ContainsInput(input)) return false;

                return KeyboardHardware.CurrentKeyDownEvent(KeyConverter.GetKey(input));
            }
            public bool IsPressed(object input)
            {
                if (!KeyConverter.ContainsInput(input)) return false;

                return KeyboardHardware.CurrentKeyDownEvent(KeyConverter.GetKey(input)) && KeyboardHardware.OldKeyUpEvent(KeyConverter.GetKey(input));
            }
            public bool IsReleased(object input)
            {
                if (!KeyConverter.ContainsInput(input)) return false;

                return KeyboardHardware.CurrentKeyUpEvent(KeyConverter.GetKey(input)) && KeyboardHardware.OldKeyDownEvent(KeyConverter.GetKey(input));
            }
        }
    }
}
