using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Hardwares.Interfaces;

namespace TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces
{
    public interface IMouseInputService : IService
    {
        public IMouseHardware MouseHardware { get; set; }
    }
}
