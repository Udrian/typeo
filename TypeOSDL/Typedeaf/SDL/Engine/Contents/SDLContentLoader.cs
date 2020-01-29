using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.SDL.Engine.Graphics;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Contents
    {
        public partial class SDLContentLoader : ContentLoader
        {
            public new SDLCanvas Canvas { get; set; }

            public SDLContentLoader(SDLCanvas canvas) : base(canvas) { }
        }
    }
}