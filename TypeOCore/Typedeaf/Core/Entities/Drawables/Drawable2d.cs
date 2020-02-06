using TypeOEngine.Typedeaf.Core.Common;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public abstract class Drawable2d : Drawable
        {
            public new Entity2d Entity { get { return base.Entity as Entity2d; } set { base.Entity = value as Entity2d; } }

            protected Drawable2d() : base() { }

            public abstract Vec2 Size { get; protected set; }
        }
    }
}
