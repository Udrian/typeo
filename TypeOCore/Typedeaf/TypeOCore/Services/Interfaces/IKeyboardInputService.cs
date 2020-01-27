using Typedeaf.TypeOCore.Engine.Hardwares.Interfaces;

namespace Typedeaf.TypeOCore
{
    namespace Services.Interfaces
    {
        public interface IKeyboardInputService : IService
        {
            public IKeyboardHardware KeyboardHardware { get; set; }

            public void SetKeyAlias(object input, object key);

            public bool IsDown(object input);
            public bool IsPressed(object input);
            public bool IsReleased(object input);
        }
    }
}
