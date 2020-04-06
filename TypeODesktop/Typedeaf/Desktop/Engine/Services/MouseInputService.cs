using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Services
    {
        public class MouseInputService : Service, IMouseInputService
        {
            public IMouseHardware MouseHardware { get; set; }

            public Vec2 MousePosition { get { return MouseHardware.CurrentMousePosition; } }
            public Vec2 MousePositionRelative { get { return MouseHardware.CurrentMousePosition - MouseHardware.OldMousePosition; } }

            public Vec2 WheelPosition { get { return MouseHardware.CurrentWheelPosition; } }
            public Vec2 WheelPositionRelative { get { return MouseHardware.CurrentWheelPosition - MouseHardware.OldWheelPosition; } }

            public override void Initialize()
            {
            }

            public override void Cleanup()
            {
            }
        }
    }
}
