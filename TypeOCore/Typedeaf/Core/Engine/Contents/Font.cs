using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Contents
    {
        public abstract class Font : Content
        {
            protected Font() { }

            public virtual int FontSize { get; set; }
            public abstract Vec2 MeasureString(string text);
        }
    }

    namespace Engine.Graphics
    {
        partial class Canvas
        {
            public abstract void DrawText(Font font, string text, Vec2 pos);
            public abstract void DrawText(Font font, string text, Vec2 pos, Vec2 scale = null, double rotate = 0, Vec2 origin = null, Color color = null, Flipped flipped = Flipped.None, Rectangle source = null);
        }
    }
}