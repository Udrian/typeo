using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Graphics;

namespace Typedeaf.TypeO.Engine
{
    namespace Graphics
    {
        public abstract class Canvas
        {
            protected Core.TypeO  TypeO   { get; set; }
            protected      Window Window  { get; set; }
            public         object Context { get; set; }

            /// <summary>
            /// Do not call directly, use Window.CreateCanvas instead
            /// </summary>
            public Canvas(Core.TypeO typeO, Window window, object context)
            {
                TypeO   = typeO;
                Window  = window;
                Context = context;
            }

            public abstract void Clear(Color clearColor);
            public abstract void DrawLine (Vec2 from, Vec2 size, Color color);
            public abstract void DrawLineE(Vec2 from, Vec2 to,   Color color);
            public abstract void DrawLines( List<Vec2> points, Color color);
            public abstract void DrawPixel(      Vec2  point,  Color color);
            public abstract void DrawPixels(List<Vec2> points, Color color);
            public abstract void DrawRectangle(Rectangle rectangle,   bool filled, Color color);
            public abstract void DrawRectangle (Vec2 from, Vec2 size, bool filled, Color color);
            public abstract void DrawRectangleE(Vec2 from, Vec2 to,   bool filled, Color color);
            public abstract void Present();

            public abstract Rectangle Viewport { get; set; }
        }

        public abstract partial class Window
        {
            public Canvas CreateCanvas(object context = null)
            {
                var canvas = TypeO.CreateCanvas?.Invoke(TypeO, this, context);
                TypeO.AddCanvas(canvas);
                return canvas;
            }

            public Canvas CreateCanvas(Rectangle rect, object context = null)
            {
                var canvas = CreateCanvas(context);
                canvas.Viewport = rect;
                return canvas;
            }
        }
    }

    namespace Core
    {
        public partial class TypeO
        {
            public delegate Canvas CreateCanvasDelegate(TypeO typeO, Window window, object context);
            public CreateCanvasDelegate CreateCanvas;
            private List<Canvas> Canvas;
            public void AddCanvas(Canvas canvas)
            {
                Canvas.Add(canvas);
            }
        }
    }
}
