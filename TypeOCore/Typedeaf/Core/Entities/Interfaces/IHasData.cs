namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Interfaces
    {
        public interface IHasData
        {
            public EntityData Data { get; set; }
        }

        public interface IHasData<D> : IHasData where D : EntityData
        {
            EntityData IHasData.Data { get => Data; set => Data = (D)value; }
            public new D Data { get; set; }
        }
    }
}
