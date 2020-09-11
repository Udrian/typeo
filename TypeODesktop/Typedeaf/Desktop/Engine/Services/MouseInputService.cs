using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.Core.Engine.Services.Input;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Services
    {
        public class MouseInputService : Service
        {
            protected IMouseHardware MouseHardware { get; set; }

            protected KeyConverter KeyConverter { get; set; }


            public Vec2 MousePosition { get { return MouseHardware.CurrentMousePosition; } }
            public Vec2 MousePositionRelative { get { return MouseHardware.CurrentMousePosition - MouseHardware.OldMousePosition; } }

            public Vec2 WheelPosition { get { return MouseHardware.CurrentWheelPosition; } }
            public Vec2 WheelPositionRelative { get { return MouseHardware.CurrentWheelPosition - MouseHardware.OldWheelPosition; } }

            public override void Initialize()
            {
                KeyConverter = new KeyConverter();
            }

            public override void Cleanup()
            {
            }

            public void SetKeyAlias(object input, object key)
            {
                KeyConverter.SetKeyAlias(input, key);
            }
            public bool IsDown(object input)
            {
                if(!KeyConverter.ContainsInput(input)) return false;

                return MouseHardware.CurrentButtonDownEvent(KeyConverter.GetKey(input));
            }
            public bool IsPressed(object input)
            {
                if(!KeyConverter.ContainsInput(input)) return false;

                return MouseHardware.CurrentButtonDownEvent(KeyConverter.GetKey(input)) && MouseHardware.OldButtonUpEvent(KeyConverter.GetKey(input));
            }
            public bool IsReleased(object input)
            {
                if(!KeyConverter.ContainsInput(input)) return false;

                return MouseHardware.CurrentButtonUpEvent(KeyConverter.GetKey(input)) && MouseHardware.OldButtonDownEvent(KeyConverter.GetKey(input));
            }
        }
    }
}
