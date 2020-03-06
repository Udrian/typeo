using TypeOEngine.Typedeaf.Core.Entities;

namespace SpaceInvader.Data.Entities
{
    public class PlanetData : EntityData, IMovementData
    {
        public double Speed { get; set; }

        public override void Initialize() { }
    }
}
