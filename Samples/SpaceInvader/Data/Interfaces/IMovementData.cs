using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace SpaceInvader.Data
{
    public interface IMovementData : IEntityData
    {
        public double Speed { get; set; }
    }
}
