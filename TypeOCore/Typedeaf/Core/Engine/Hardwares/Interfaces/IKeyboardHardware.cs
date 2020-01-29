namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Hardwares.Interfaces
    {
        public interface IKeyboardHardware : IHardware
        {
            public bool CurrentKeyDownEvent(object key);
            public bool CurrentKeyUpEvent(object key);
            public bool OldKeyDownEvent(object key);
            public bool OldKeyUpEvent(object key);
        }
    }
}
