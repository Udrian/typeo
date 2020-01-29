namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Interfaces
    {
        public interface IHasTypeO
        {
            public TypeO TypeO { get; set; }

            public void SetTypeO(TypeO typeO)
            {
                TypeO = typeO;
            }
        }
    }
}
