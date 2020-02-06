﻿using TypeOEngine.Typedeaf.Core.Engine.Graphics;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Drawables
    {
        public abstract class Drawable
        {
            public Entity Entity { get; set; }

            protected Drawable() { }

            public abstract void Initialize();
            public abstract void Draw(Canvas canvas);
        }
    }
}
