namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity
        {
            protected Entity() {}

            public abstract void Initialize();

            public void Remove()
            {
                WillBeDeleted = true;
            }
            public bool WillBeDeleted { get; private set; }
        }
    }
}