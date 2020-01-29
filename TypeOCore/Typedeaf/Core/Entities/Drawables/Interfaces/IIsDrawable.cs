using TypeOEngine.Typedeaf.Core.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables.Interfaces
    {
        public interface IIsDrawable
        {
            void Draw(Canvas canvas);
        }

        public interface IIsDrawable<C> where C : Canvas
        {
            void Draw(C canvas);
        }
    }
}
