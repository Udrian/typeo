using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public class DrawableTexture : Drawable2d
        {
            public Texture Texture { get; set; }
            public override Vec2 Size { get { return Texture.Size; } protected set { } }

            public DrawableTexture() : base() { }

            public override void Initialize() { }

            public override void Draw(Canvas canvas)
            {
                if (Texture == null) return;
                canvas.DrawImage(
                    Texture,
                    Vec2.Zero,
                    entity: Entity,
                    color: Entity.Color,
                    flipped: Entity.Flipped
                );
            }
        }
    }
}
