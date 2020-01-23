using Typedeaf.TypeOCore.Contents;
using Typedeaf.TypeOSDL.Graphics;

namespace Typedeaf.TypeOSDL
{
    namespace Contents
    {
        public partial class SDLContentLoader : ContentLoader
        {
            public new SDLCanvas Canvas { get; set; }

            public SDLContentLoader(SDLCanvas canvas) : base(canvas) { }
        }
    }
}