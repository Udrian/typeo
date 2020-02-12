using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Interfaces
    {
        public interface IHasDrawable
        {
            public bool Hidden { get; set; }
            public Drawable Drawable { get; set; }

            public void CreateDrawable(Entity entity);
        }

        public interface IHasDrawable<D> : IHasDrawable where D : Drawable, new()
        {
            Drawable IHasDrawable.Drawable { get { return Drawable; } set { Drawable = value as D; } }
            public new D Drawable { get; set; }

            void IHasDrawable.CreateDrawable(Entity entity)
            {
                Drawable = new D
                {
                    Entity = entity
                };
            }
        }
    }
}
