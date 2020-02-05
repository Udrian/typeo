using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace SpaceInvader.Entities.Data
{
    public interface IMovementData : IEntityData
    {
        public double Speed { get; set; }
    }
}
