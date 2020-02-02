using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Interfaces
    {
        public interface IHasDrawable
        {
            public bool Hidden { get; set; }
            public void DrawDrawable(Entity entity, Canvas canvas);
        }

        public interface IHasDrawable<D> : IHasDrawable where D : Drawable
        {
            public D Drawable { get; set; }
            void IHasDrawable.DrawDrawable(Entity entity, Canvas canvas)
            {
                Drawable.Draw(entity, canvas);
            }
        }
    }
}
