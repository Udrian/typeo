using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;

namespace TypeOEngine.Typedeaf.Basic
{
    namespace Services.Interfaces
    {
        public interface ICamera2dService : IService
        {
            public Canvas Canvas { get; set; }
            public Vec2 Position { get; set; }
        }
    }
}
