using TypeOEngine.Typedeaf.Core.Entities;

namespace SpaceInvader.Data.Scenes
{
    public class PlanetSceneData : EntityData
    {
        public int AlienSpawns { get; set; } = 100;
        public double AlienSpawnTimer { get; set; } = 0;
        public double AlienSpawnTime { get; set; } = 0.25;

        public override void Initialize()
        {
        }
    }
}
