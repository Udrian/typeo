using Typedeaf.TypeOCommon;
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
            public abstract void DrawImage(Texture texture, Vec2 pos, Vec2 scale, double rotation, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source);
            public abstract void DrawImage(Texture texture, Vec2 pos, Vec2? scale = null, double rotation = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null);
        }
    }
}