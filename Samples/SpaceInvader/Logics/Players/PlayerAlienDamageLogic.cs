using SpaceInvader.Entities;
using SpaceInvader.Scenes;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics.Players
{
    public class PlayerAlienDamageLogic : Logic, IHasEntity<Player>, IHasScene, IHasGame<SpaceInvaderGame>
    {
        public Player Entity { get; set; }
        public Scene Scene { get; set; }
        public SpaceInvaderGame Game { get; set; }

        public override void Initialize() { }

        public override void Update(double dt)
        {
            foreach (var alien in Scene.Entities.List<Alien>())
            {
                if (alien.WillBeDeleted) continue;
                if (alien.Position.X <= Entity.Position.X + Entity.Size.X && (alien.Position.X + alien.Size.X) >= Entity.Position.X &&
                    alien.Position.Y <= Entity.Position.Y + Entity.Size.Y && (alien.Position.Y + alien.Size.Y) >= Entity.Position.Y)
                {
                    alien.Remove();
                    Entity.EntityData.Health--;
                    if (Entity.EntityData.Health <= 0)
                    {
                        if(Scene is SpaceScene)
                            Scene.Scenes.SetScene<ScoreScene>();
                        else
                            Scene.Scenes.SetScene<SpaceScene>();
                    }
                }
            }
        }
    }
}
