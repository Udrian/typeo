using TypeOEngine.Typedeaf.Core.Entities;

namespace SpaceInvader.Entities.Data
{
    public class PlanetData : EntityData, IMovementData
    {
        public double Speed { get; set; }

        public override void Initialize() { }
    }
}
