using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public class DrawableTexture : Drawable2d
        {
            public Texture Texture { get; set; }

            public DrawableTexture(Texture texture) : base()
            {
                Texture = texture;
            }

            public override void Init() { }

            public override void Draw(Entity2d entity, Canvas canvas)
            {
                //TODO: Fix Drawing bounds and screen bounds
                canvas.DrawImage(
                    Texture,
                    entity.Position,
                    entity.Scale,
                    entity.Rotation,
                    entity.Origin,
                    entity.Color,
                    entity.Flipped
                );
            }

            public override Vec2 GetSize()
            {
                return new Vec2(Texture.Size.X, Texture.Size.Y);
            }
        }
    }
}
