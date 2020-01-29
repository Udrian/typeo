using TypeOEngine.Typedeaf.Core.Engine;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Interfaces
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
