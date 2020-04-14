using TypeOEngine.Typedeaf.Core.Engine.Services;
using TypeOEngine.Typedeaf.SDL.Engine.Services.Interfaces;

namespace TypeOEngine.Typedeaf.SDL.Engine.Services
{
    internal class SDLService : Service, ISDLService
    {
        public SDLModuleOption Option { get; set; }

        public override void Initialize()
        {
        }

        public override void Cleanup()
        {
        }
    }
}
