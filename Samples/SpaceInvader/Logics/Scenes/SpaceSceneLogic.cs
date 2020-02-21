using SpaceInvader.Entities;
using SpaceInvader.Scenes;
using System;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics.Scenes
{
    public class SpaceSceneLogic : Logic, IHasScene<SpaceScene>, IHasGame<SpaceInvaderGame>
    {
        public ILogger Logger { get; set; }
        public SpaceScene Scene { get; set; }
        public SpaceInvaderGame Game { get; set; }

        public double AlienSpawnTimer { get; set; } = 0;
        public double AlienSpawnTime { get; set; } = 5;
        public double AlienSpawnFrequencyTimer { get; set; } = 0;
        public double AlienSpawnFrequencyTime { get; set; } = 0.25;
        public int AlienSpawnAmount { get; set; } = 4;
        public int AlienSpawns { get; set; } = 0;
        public bool AlienSpawning { get; set; } = false;
        public double AlienSpawnPhase { get; set; } = 0;

        public double PlanetSpawnTimer { get; set; } = 0;
        public double PlanetSpawnTime { get; set; } = 30;
        public bool PlanetSpawned { get; set; }

        public override void Initialize()
        {
            if (Scene == null)
            {
                Logger.Log(LogLevel.Warning, "Scene is null in SpaceSceneLogic");
            }
        }

        public override void Update(double dt)
        {
            if (!PlanetSpawned)
            {
                PlanetSpawnTimer += dt;
                if (PlanetSpawnTimer >= PlanetSpawnTime)
                {
                    PlanetSpawnTimer = 0;
                    PlanetSpawned = true;
                    Scene.Entities.Create<Planet>();
                }
            }

            if (!AlienSpawning)
            {
                AlienSpawnTimer += dt;
                if (AlienSpawnTimer >= AlienSpawnTime)
                {
                    AlienSpawnTimer -= AlienSpawnTime;
                    AlienSpawning = true;
                    AlienSpawnPhase = Game.Random.NextDouble() * Math.PI * 3;
                }
            }
            else
            {
                AlienSpawnFrequencyTimer += dt;
                if (AlienSpawnFrequencyTimer >= AlienSpawnFrequencyTime)
                {
                    AlienSpawnFrequencyTimer -= AlienSpawnFrequencyTime;
                    var alien = Scene.Entities.Create<Alien>();
                    alien.EntityData.Phase = AlienSpawnPhase;

                    AlienSpawns++;
                    if (AlienSpawns >= AlienSpawnAmount)
                    {
                        AlienSpawning = false;
                        AlienSpawnFrequencyTimer = 0;
                        AlienSpawns = 0;
                    }
                }
            }
            EntityUpdate(dt);
        }

        private void EntityUpdate(double dt)
        {
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
