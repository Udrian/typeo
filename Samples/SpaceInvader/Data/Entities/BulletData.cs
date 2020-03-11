using TypeOEngine.Typedeaf.Core.Entities;

namespace SpaceInvader.Data.Entities
{
    class BulletData : EntityData, IMovementData
    {
        public double Speed { get; set; }

        public override void Initialize() { }
    }
}
