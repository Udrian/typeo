using Typedeaf.TypeOCore;
using Typedeaf.TypeOSDL.Graphics;

namespace Typedeaf.TypeOSDL
{
    public abstract class SDLScene<G> : Scene<G, SDLCanvas> where G : Game
    {
        public SDLScene(G game, SDLCanvas canvas) : base(game, canvas) { }
    }
}
