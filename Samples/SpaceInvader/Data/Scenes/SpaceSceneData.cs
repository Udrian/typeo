using TypeOEngine.Typedeaf.Core.Entities;

namespace SpaceInvader.Data.Scenes
{
    class SpaceSceneData : EntityData
    {
        public double AlienSpawnTimer { get; set; } = 0;
        public double AlienSpawnTime { get; set; } = 5;
        public double AlienSpawnFrequencyTimer { get; set; } = 0;
        public double AlienSpawnFrequencyTime { get; set; } = 0.5;
        public int AlienSpawnAmount { get; set; } = 4;
        public int AlienSpawns { get; set; } = 0;
        public bool AlienSpawning { get; set; } = false;
        public double AlienSpawnPhase { get; set; } = 0;

        public double AlienRunnerSpawnTimer { get; set; } = 0;
        public double AlienRunnerSpawnTime { get; set; } = 10;
        public double AlienRunnerSpawnFrequencyTimer { get; set; } = 0;
        public double AlienRunnerSpawnFrequencyTime { get; set; } = 2;
        public int AlienRunnerSpawnAmount { get; set; } = 10;
        public int AlienRunnerSpawns { get; set; } = 0;
        public bool AlienRunnerSpawning { get; set; } = false;

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
