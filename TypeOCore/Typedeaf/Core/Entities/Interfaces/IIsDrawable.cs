using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Interfaces
    {
        public interface IIsDrawable
        {
            public bool Hidden { get; set; }
            public void Draw(Canvas canvas);
        }

        public interface IIsDrawable<C> : IIsDrawable where C : Canvas
        {
            void IIsDrawable.Draw(Canvas canvas) { Draw(canvas as C); }
            public void Draw(C canvas);
        }
    }
}
