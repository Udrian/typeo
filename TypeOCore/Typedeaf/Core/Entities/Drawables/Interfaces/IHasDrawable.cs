using TypeOEngine.Typedeaf.Core.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables.Interfaces
    {
        public interface IHasDrawable
        {
            public void DrawDrawable(Canvas canvas);
        }

        public interface IHasDrawable<D> : IHasDrawable where D : Drawable
        {
            public D Drawable { get; set; }
            void IHasDrawable.DrawDrawable(Canvas canvas)
            {
                Drawable.Draw(canvas);
            }
        }
    }
}
