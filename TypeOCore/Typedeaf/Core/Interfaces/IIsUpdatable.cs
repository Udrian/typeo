namespace TypeOEngine.Typedeaf.Core
{
    namespace Interfaces
    {
        public interface IIsUpdatable
        {
            public bool Pause { get; set; }
            void Update(double dt);
        }
    }
}