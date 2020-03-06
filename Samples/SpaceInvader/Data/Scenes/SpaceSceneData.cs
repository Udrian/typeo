using TypeOEngine.Typedeaf.Core.Entities;

namespace SpaceInvader.Data.Scenes
{
    public class SpaceSceneData : EntityData
    {
        public double AlienSpawnTimer { get; set; } = 0;
        public double AlienSpawnTime { get; set; } = 5;
        public double AlienSpawnFrequencyTimer { get; set; } = 0;
        public double AlienSpawnFrequencyTime { get; set; } = 0.5;
        public int AlienSpawnAmount { get; set; } = 4;
        public int AlienSpawns { get; set; } = 0;
        public bool AlienSpawning { get; set; } = false;
        public double AlienSpawnPhase { get; set; } = 0;

        public double PlanetSpawnTimer { get; set; } = 0;
        public double PlanetSpawnTime { get; set; } = 30;
        public bool PlanetSpawned { get; set; }

        public double PowerUpSpawnTimer { get; set; } = 0;
        public double PowerUpSpawnTime { get; set; } = 60;

        public override void Initialize()
        {
        }
    }
}
