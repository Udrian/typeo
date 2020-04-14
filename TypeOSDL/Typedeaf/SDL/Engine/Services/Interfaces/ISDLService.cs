using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Services.Interfaces
    {
        internal interface ISDLService : IService
        {
            public SDLModuleOption Option { get; set; }
        }
    }
}
