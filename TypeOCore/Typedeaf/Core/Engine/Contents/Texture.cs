using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Contents
    {
        public abstract class Texture : Content
        {
            protected Texture() { }

            public Vec2 Size { get; protected set; }
        }
    }

    namespace Engine.Graphics
    {
        partial class Canvas
        {
            public abstract void DrawImage(Texture texture, Vec2 pos, Entity2d entity = null);
            public abstract void DrawImage(Texture texture, Vec2 pos, Vec2 scale = null, double rotation = 0, Vec2 origin = null, Color color = null, Flipped flipped = Flipped.None, Rectangle source = null, Entity2d entity = null);
        }
    }
}