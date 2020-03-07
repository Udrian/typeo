using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;

namespace SpaceInvader.Entities
{
    class PlayerGround : Player
    {
        public override void LoadContent()
        {
            Drawable.Texture = Scene.ContentLoader.LoadContent<Texture>("content/player_ground.png");
        }

        public override void Shoot()
        {
            Scene.Entities.Create<Bullet>(new Vec2(Position.X + Size.X / 2 - 2, Position.Y)).EntityData.Speed += EntityData.Speed;
        }
    }
}
