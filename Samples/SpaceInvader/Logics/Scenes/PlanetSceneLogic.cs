using SpaceInvader.Entities;
using SpaceInvader.Scenes;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics.Scenes
{
    public class PlanetSceneLogic : Logic, IHasScene<PlanetScene>, IHasGame<SpaceInvaderGame>
    {
        public ILogger Logger { get; set; }
        public PlanetScene Scene { get; set; }
        public SpaceInvaderGame Game { get; set; }

        public int AlienSpawns { get; set; } = 100;
        public double AlienSpawnTimer { get; set; } = 0;
        public double AlienSpawnTime { get; set; } = 0.25;

        public override void Initialize()
        {
            if(Scene == null)
            {
                Logger.Log(LogLevel.Warning, "Scene is null in PlanetSceneLogic");
            }
        }

        public override void Update(double dt)
        {
            if (AlienSpawns > 0)
            {
                AlienSpawnTimer += dt;
                if (AlienSpawnTimer >= AlienSpawnTime)
                {
                    AlienSpawnTimer -= AlienSpawnTime;
                    Scene.Entities.Create<AlienGround>();
                    AlienSpawns--;
                }
            }
            if (AlienSpawns == 0 && Scene.Entities.List<AlienGround>().Count == 0)
            {
                Scene.Scenes.SetScene<SpaceScene>();
            }

            foreach (var alien in Scene.Entities.List<AlienGround>())
            {
                foreach (var bullet in Scene.Entities.List<Bullet>())
                {
                    if (alien.Position.X <= bullet.Position.X && (alien.Position.X + alien.Size.X) >= bullet.Position.X &&
                        alien.Position.Y <= bullet.Position.Y && (alien.Position.Y + alien.Size.Y) >= bullet.Position.Y)
                    {
                        bullet.Remove();
                        alien.EntityData.Health--;
                        if (alien.EntityData.Health <= 0)
                        {
                            alien.Remove();
                            Game.Score++;
                        }
                    }
                }

                if (alien.Position.X <= Scene.Player.Position.X + Scene.Player.Size.X && (alien.Position.X + alien.Size.X) >= Scene.Player.Position.X &&
                    alien.Position.Y <= Scene.Player.Position.Y + Scene.Player.Size.Y && (alien.Position.Y + alien.Size.Y) >= Scene.Player.Position.Y)
                {
                    alien.Remove();
                    Scene.Player.EntityData.Health--;
                    if (Scene.Player.EntityData.Health <= 0)
                    {
                        Scene.Scenes.SetScene<SpaceScene>();
                    }
                }
            }
        }
    }
}
