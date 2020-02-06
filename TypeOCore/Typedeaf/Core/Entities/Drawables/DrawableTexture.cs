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
                //TODO: Fix Drawing bounds and screen bounds
                canvas.DrawImage(
                    Texture,
                    Entity.Position,
                    Entity.Scale,
                    Entity.Rotation,
                    Entity.Origin,
                    Entity.Color,
                    Entity.Flipped
                );
            }
        }
    }
}
