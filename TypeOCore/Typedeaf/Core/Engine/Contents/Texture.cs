using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Contents
    {
        public abstract class Texture : Content
        {
            public enum Flipped
            {
                None,
                Horizontal,
                Vertical,
                Both
            }

            protected Texture() { }

            public Vec2 Size { get; protected set; }
        }
    }

    namespace Engine.Graphics
    {
        partial class Canvas
        {
            public abstract void DrawImage(Texture texture, Vec2 pos);
            public abstract void DrawImage(Texture texture, Vec2 pos, Vec2 scale = null, double rotation = 0, Vec2 origin = null, Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null);
        }
    }
}