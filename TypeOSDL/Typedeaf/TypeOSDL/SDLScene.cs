using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Graphics;

namespace Typedeaf.TypeOSDL
{
    public abstract class SDLScene : Scene<SDLCanvas>
    {
        public SDLScene(SDLCanvas canvas) : base(canvas) { }
    }
}