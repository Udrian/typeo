using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOSDL.Content;
using Typedeaf.TypeOSDL.Graphics;

namespace Typedeaf.TypeOSDL
{
    namespace Content
    {
        public partial class SDLContentLoader : ContentLoader
        {
            protected SDLCanvas Canvas { get; set; }

            public SDLContentLoader(string basePath, SDLCanvas canvas) : base(basePath)
            {
                Canvas = canvas;
            }
        }
    }

    public static partial class SDLGame
    {
        public static SDLContentLoader CreateContentLoader(this Game game, string basePath, SDLCanvas canvas)
        {
            return game.CreateContentLoader<SDLContentLoader>(basePath, canvas);
        }
    }
}