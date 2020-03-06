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
    public class SpaceSpawnLogic : Logic, IHasScene<SpaceScene>, IHasGame<SpaceInvaderGame>, IHasData<SpaceSceneData>
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
                    var alien = Scene.Entities.CreateFromStub<AlienGrunt, Alien>();
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

            EntityData.PowerUpSpawnTimer += dt;
            if(EntityData.PowerUpSpawnTimer >= EntityData.PowerUpSpawnTime)
            {
                EntityData.PowerUpSpawnTimer -= EntityData.PowerUpSpawnTime;
                Scene.Entities.Create<PowerUp>();
            }
        }
    }
}
