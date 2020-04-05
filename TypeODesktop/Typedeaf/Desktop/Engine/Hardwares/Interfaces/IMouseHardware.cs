using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Hardwares.Interfaces;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Hardwares.Interfaces
    {
        public interface IMouseHardware : IHardware
        {
            public Vec2 CurrentMousePosition { get; set; }
            public Vec2 OldMousePosition { get; set; }
            public Vec2 CurrentWheelPosition { get; set; }
            public Vec2 OldWheelPosition { get; set; }

            public bool CurrentButtonDownEvent(object key);
            public bool CurrentButtonUpEvent(object key);
            public bool OldButtonDownEvent(object key);
            public bool OldButtonUpEvent(object key);
        }
    }
}
