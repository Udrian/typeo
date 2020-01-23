using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Contents;

namespace Typedeaf.TypeOCore
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
            public abstract void DrawText(Font font, string text, Vec2 pos, Vec2 scale, double rotate, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source);
            public abstract void DrawText(Font font, string text, Vec2 pos, Vec2? scale = null, double rotate = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null);
        }
    }
}