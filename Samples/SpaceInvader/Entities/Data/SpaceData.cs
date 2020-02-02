using TypeOEngine.Typedeaf.Core.Entities;

namespace SpaceInvader.Entities.Data
{
    public class SpaceData : EntityData
    {
        public int NumberOfStars { get; set; }
        public double Speed { get; set; }

        public override void Initialize() { }
    }
}
