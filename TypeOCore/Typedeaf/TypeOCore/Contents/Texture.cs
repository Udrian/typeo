using Typedeaf.Common;
using Typedeaf.TypeOCore.Contents;

namespace Typedeaf.TypeOCore
{
    namespace Contents
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

    namespace Graphics
    {
        public abstract partial class Canvas
        {
            public abstract void DrawImage(Texture texture, Vec2 pos);
            public abstract void DrawImage(Texture texture, Vec2 pos, Vec2 scale = null, double rotation = 0, Vec2 origin = null, Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null);
        }
    }
}