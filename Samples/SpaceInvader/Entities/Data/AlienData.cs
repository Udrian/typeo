using TypeOEngine.Typedeaf.Core.Entities;

namespace SpaceInvader.Entities.Data
{
    public class AlienData : EntityData
    {
        public double SinTime { get; set; }
        public double Amplitude { get; set; }
        public double Frequency { get; set; }
        public double Phase { get; set; }

        public double Speed { get; set; }
        public int Health { get; set; }

        public override void Initialize() { }
    }
}
