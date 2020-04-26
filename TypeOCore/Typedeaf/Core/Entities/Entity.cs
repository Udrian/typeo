using TypeOEngine.Typedeaf.Core.Engine;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity
        {
            public string ID { get; internal set; }
            public Entity Parent { get; internal set; }
            internal EntityList ParentEntityList { get; set; } //TODO: This should change to something else

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