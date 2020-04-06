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
            public override void Initialize()
            {
            }

            public override void Cleanup()
            {
            }
        }
    }
}
