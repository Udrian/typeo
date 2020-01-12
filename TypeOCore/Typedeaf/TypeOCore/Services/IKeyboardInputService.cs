namespace Typedeaf.TypeOCore
{
    namespace Services
    {
        public interface IKeyboardInputService
        {
            public void SetKeyAlias(object input, object key);

            public bool IsDown(object input);
            public bool IsPressed(object input);
            public bool IsReleased(object input);
        }
    }
}
