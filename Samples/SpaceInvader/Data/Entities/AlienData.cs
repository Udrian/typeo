using TypeOEngine.Typedeaf.Core.Entities;

namespace SpaceInvader.Data.Entities
{
    class AlienData : EntityData, IMovementData
    {
        public double Speed { get; set; }
        public int Health { get; set; }

        public override void Initialize() { }
    }
}
