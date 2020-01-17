using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Entities.Drawables
    {
        public class DrawableTexture : Drawable2d
        {
            public Texture Texture { get; set; }

            public DrawableTexture(Entity2d entity, Texture texture) : base(entity)
            {
                Texture = texture;
            }

            public override void Init(Entity2d entity) { }

            public override void Draw(Canvas canvas)
            {
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

            public override Vec2 GetSize()
            {
                return new Vec2(Texture.Size.X, Texture.Size.Y);
            }
        }
    }
}
