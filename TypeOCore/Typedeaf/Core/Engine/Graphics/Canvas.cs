using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Entities;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine.Graphics
    {
        public abstract partial class Canvas
        {
            public Window Window { get; set; }
            public abstract Rectangle Viewport { get; set; }

            protected Canvas() {}

            public abstract void Initialize();

            public abstract void Clear(Color clearColor);
            public abstract void DrawLine (Vec2 from, Vec2 size, Color color, Entity2d entity = null);
            public abstract void DrawLineE(Vec2 from, Vec2 to,   Color color, Entity2d entity = null);
            public abstract void DrawLines( List<Vec2> points, Color color, Entity2d entity = null);
            public abstract void DrawPixel(      Vec2  point,  Color color, Entity2d entity = null);
            public abstract void DrawPixels(List<Vec2> points, Color color, Entity2d entity = null);
            public abstract void DrawRectangle(Rectangle rectangle,   bool filled, Color color, Entity2d entity = null);
            public abstract void DrawRectangle (Vec2 from, Vec2 size, bool filled, Color color, Entity2d entity = null);
            public abstract void DrawRectangleE(Vec2 from, Vec2 to,   bool filled, Color color, Entity2d entity = null);

            public abstract void Present();
        }
    }
}
