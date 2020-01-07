using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOSDL.Content;
using Typedeaf.TypeOSDL.Graphics;

namespace Typedeaf.TypeOSDL
{
    namespace Content
    {
        public partial class SDLContentLoader : ContentLoader
        {
            public SDLCanvas Canvas { get; set; }

            public SDLContentLoader() : base() { }
        }
    }

    public partial class SDLScene
    {
        public void CreateContentLoader(string basePath)
        {
            CreateContentLoader<SDLContentLoader>(basePath);
            if(ContentLoader is SDLContentLoader)
                (ContentLoader as SDLContentLoader).Canvas = Canvas as SDLCanvas;
        }
    }
}