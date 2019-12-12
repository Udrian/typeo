using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Graphics;

namespace Typedeaf.TypeOSDL
{
    public abstract partial class SDLScene : Scene
    {
        public SDLCanvas CreateCanvas()
        {
            return CreateCanvas<SDLCanvas>();
        }

        public SDLCanvas CreateCanvas(Rectangle rect)
        {
            return CreateCanvas<SDLCanvas>(rect);
        }
    }
}