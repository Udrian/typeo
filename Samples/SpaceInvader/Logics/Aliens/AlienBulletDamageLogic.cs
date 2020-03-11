using SpaceInvader.Entities;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics.Aliens
{
    class AlienBulletDamageLogic : Logic, IHasEntity<Alien>, IHasScene, IHasGame<SpaceInvaderGame>
    {
        public Alien Entity { get; set; }
        public Scene Scene { get; set; }
        public SpaceInvaderGame Game { get; set; }

        public override void Initialize() { }

        public override void Update(double dt)
        {
            foreach (var bullet in Scene.Entities.List<Bullet>())
            {
                if (bullet.WillBeDeleted) continue;

                var r1x = Entity.Position.X;
                var r1y = Entity.Position.Y;
                var r1w = Entity.Size.X;
                var r1h = Entity.Size.Y;

                var r2x = bullet.Position.X;
                var r2y = bullet.Position.Y;
                var r2w = bullet.Size.X;
                var r2h = bullet.Size.Y;

                if (r1x + r1w >= r2x &&
                    r1x <= r2x + r2w &&
                    r1y + r1h >= r2y &&
                    r1y <= r2y + r2h)
                {
                    bullet.Remove();
                    Entity.EntityData.Health--;
                    if (Entity.EntityData.Health <= 0)
                    {
                        Entity.Remove();
                        Game.Score++;
                        break;
                    }
                }
            }
        }
    }
}
