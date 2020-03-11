using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;

namespace TypeOEngine.Typedeaf.Desktop
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
