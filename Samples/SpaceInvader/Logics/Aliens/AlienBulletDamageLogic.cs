using SpaceInvader.Entities;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics.Aliens
{
    public class AlienBulletDamageLogic : Logic, IHasEntity<Alien>, IHasScene, IHasGame<SpaceInvaderGame>
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
                if (Entity.Position.X <= bullet.Position.X && (Entity.Position.X + Entity.Size.X) >= bullet.Position.X &&
                    Entity.Position.Y <= bullet.Position.Y && (Entity.Position.Y + Entity.Size.Y) >= bullet.Position.Y)
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
