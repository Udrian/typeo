using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Contents;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Contents
    {
        public abstract class Font : Content
        {
            protected Font() { }

            public virtual int FontSize { get; set; }
            public abstract Vec2 MeasureString(string text);
        }
    }

    namespace Graphics
    {
        public abstract partial class Canvas
        {
            public abstract void DrawText(Font font, string text, Vec2 pos);
            public abstract void DrawText(Font font, string text, Vec2 pos, Vec2 scale = null, double rotate = 0, Vec2 origin = null, Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null);
        }
    }
}