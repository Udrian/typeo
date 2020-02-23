namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity
        {
            public string ID { get; set; }
            public Entity Parent { get; set; }

            public Entity() { }

            public abstract void Initialize();
            public abstract void Cleanup();

            public void Remove()
            {
                WillBeDeleted = true;
            }
            public bool WillBeDeleted { get; private set; }
        }
    }
}