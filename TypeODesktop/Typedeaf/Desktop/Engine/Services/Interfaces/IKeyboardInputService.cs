using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Services.Interfaces
    {
        public interface IKeyboardInputService : IService
        {
            public void SetKeyAlias(object input, object key);

            public bool IsDown(object input);
            public bool IsPressed(object input);
            public bool IsReleased(object input);
        }
    }
}
