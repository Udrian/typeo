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

    public partial class SDLGame
    {
        public SDLContentLoader CreateContentLoader(string basePath, SDLCanvas canvas)
        {
            return CreateContentLoader<SDLContentLoader>(basePath, canvas);
        }
    }
}