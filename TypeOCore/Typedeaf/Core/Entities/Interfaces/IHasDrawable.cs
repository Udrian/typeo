using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Interfaces
    {
        public interface IHasDrawable
        {
            public bool Hidden { get; set; }
            public Drawable Drawable { get; set; }
            public void DrawDrawable(Entity entity, Canvas canvas);
            public void CreateDrawable(Entity entity);
        }

        public interface IHasDrawable<D> : IHasDrawable where D : Drawable, new()
        {

            Drawable IHasDrawable.Drawable { get { return Drawable; } set { Drawable = value as D; } }
            public new D Drawable { get; set; }
            void IHasDrawable.DrawDrawable(Entity entity, Canvas canvas)
            {
                Drawable.Draw(entity, canvas);
            }
            void IHasDrawable.CreateDrawable(Entity entity)
            {
                Drawable = new D();

                (Drawable as IHasGame)?.SetGame((entity as IHasGame)?.Game);

                Drawable.Init();
            }
        }
    }
}
