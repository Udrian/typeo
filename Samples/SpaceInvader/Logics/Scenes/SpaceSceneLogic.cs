using SpaceInvader.Data.Scenes;
using SpaceInvader.Entities;
using SpaceInvader.Entities.Aliens;
using SpaceInvader.Logics.Aliens;
using SpaceInvader.Scenes;
using System;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics.Scenes
{
    public class SpaceSceneLogic : Logic, IHasScene<SpaceScene>, IHasGame<SpaceInvaderGame>, IHasData<SpaceSceneData>
    {
        public SpaceScene Scene { get; set; }
        public SpaceInvaderGame Game { get; set; }
        public SpaceSceneData EntityData { get; set; }

        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            if (!EntityData.PlanetSpawned)
            {
                EntityData.PlanetSpawnTimer += dt;
                if (EntityData.PlanetSpawnTimer >= EntityData.PlanetSpawnTime)
                {
                    EntityData.PlanetSpawnTimer = 0;
                    EntityData.PlanetSpawned = true;
                    Scene.Entities.Create<Planet>();
                }
            }

            if (!EntityData.AlienSpawning)
            {
                EntityData.AlienSpawnTimer += dt;
                if (EntityData.AlienSpawnTimer >= EntityData.AlienSpawnTime)
                {
                    EntityData.AlienSpawnTimer -= EntityData.AlienSpawnTime;
                    EntityData.AlienSpawning = true;
                    EntityData.AlienSpawnPhase = Game.Random.NextDouble() * Math.PI * 3;
                }
            }
            else
            {
                EntityData.AlienSpawnFrequencyTimer += dt;
                if (EntityData.AlienSpawnFrequencyTimer >= EntityData.AlienSpawnFrequencyTime)
                {
                    EntityData.AlienSpawnFrequencyTimer -= EntityData.AlienSpawnFrequencyTime;
                    var alien = Scene.Entities.CreateFromStub<Grunt, Alien>();
                    alien.Logic.GetLogic<AlienSwayLogic>().Phase = EntityData.AlienSpawnPhase;

                    EntityData.AlienSpawns++;
                    if (EntityData.AlienSpawns >= EntityData.AlienSpawnAmount)
                    {
                        EntityData.AlienSpawning = false;
                        EntityData.AlienSpawnFrequencyTimer = 0;
                        EntityData.AlienSpawns = 0;
                    }
                }
            }

            foreach (var alien in Scene.Entities.List<Alien>())
            {
                if (alien.WillBeDeleted) continue;
                foreach (var bullet in Scene.Entities.List<Bullet>())
                {
                    if (bullet.WillBeDeleted) continue;
                    if (alien.Position.X <= bullet.Position.X && (alien.Position.X + alien.Size.X) >= bullet.Position.X &&
                        alien.Position.Y <= bullet.Position.Y && (alien.Position.Y + alien.Size.Y) >= bullet.Position.Y)
                    {
                        bullet.Remove();
                        alien.EntityData.Health--;
                        if (alien.EntityData.Health <= 0)
                        {
                            alien.Remove();
                            Game.Score++;
                            break;
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
                        Scene.Scenes.SetScene<ScoreScene>();
                    }
                }
            }
        }
    }
}
