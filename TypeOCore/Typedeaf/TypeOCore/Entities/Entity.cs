namespace Typedeaf.TypeOCore
{
    namespace Entities
    {
        public interface IIsUpdatable
        {
            void Update(float dt);
        }

        public abstract class Entity
        {
            public Entity() {}
        }
    }
}