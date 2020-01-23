namespace Typedeaf.TypeOCore
{
    namespace Entities
    {
        public interface IIsUpdatable
        {
            void Update(double dt);
        }

        public abstract class Entity
        {
            public Entity() {}

            public abstract void Initialize();
        }
    }
}