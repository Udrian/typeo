using SpaceInvader.Data.Scenes;
using SpaceInvader.Entities;
using SpaceInvader.Entities.Aliens;
using SpaceInvader.Scenes;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace SpaceInvader.Logics.Scenes
{
    class PlanetSpawnLogic : Logic, IHasScene<PlanetScene>, IHasGame<SpaceInvaderGame>, IHasData<PlanetSceneData>
    {
        public PlanetScene Scene { get; set; }
        public SpaceInvaderGame Game { get; set; }
        public PlanetSceneData EntityData { get; set; }

        public override void Initialize()
        {
        }

        public override void Update(double dt)
        {
            if (EntityData.AlienSpawns > 0)
            {
                EntityData.AlienSpawnTimer += dt;
                if (EntityData.AlienSpawnTimer >= EntityData.AlienSpawnTime)
                {
                    EntityData.AlienSpawnTimer -= EntityData.AlienSpawnTime;
                    Scene.Entities.CreateFromStub<AlienRunner>();
                    EntityData.AlienSpawns--;
                }
            }
            if (EntityData.AlienSpawns == 0 && Scene.Entities.List<Alien>().Count == 0)
            {
                Scene.Scenes.SetScene<SpaceScene>();
            }
        }
    }
}
