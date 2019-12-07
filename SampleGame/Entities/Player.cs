using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Entities;
using Typedeaf.TypeOCore.Entities.Drawables;
using Typedeaf.TypeOCommon;

namespace SampleGameCore.Entities
{
    public class Player : Entity2d, IHasGame<SpaceInvaderGame>, IHasDrawable<DrawableTexture>, IIsUpdatable
    {
        public SpaceInvaderGame Game { get; set; }
        public DrawableTexture Drawable { get; set; }

        public float Speed { get; set; } = 3f;

        public override void Initialize()
        {
            Drawable = new DrawableTexture(this, Game.ContentLoader.LoadTexture("content/image.png"));
            Position = new Vec2(100, 400);
        }

        public void Update(float dt)
        {
            if (Game.Input.Key.IsDown("Left"))
            {
                Position += new Vec2(-Speed, 0);
            }
            if (Game.Input.Key.IsDown("Right"))
            {
                Position += new Vec2(Speed, 0);
            }
        }
    }
}
