namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Interfaces
    {
        public interface IHasData
        {
            public EntityData EntityData { get; set; }
        }

        public interface IHasData<D> : IHasData where D : EntityData
        {
            EntityData IHasData.EntityData { get => EntityData; set => EntityData = (D)value; }
            public new D EntityData { get; set; }
        }
    }
}
