using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Graphics
    {
        public abstract partial class Canvas
        {
            protected TypeO  TypeO   { get; set; }
            protected Window Window  { get; set; }
            public    object Context { get; set; }

            /// <summary>
            /// Do not call directly, use Window.CreateCanvas instead
            /// </summary>
            public Canvas(TypeO typeO, Window window, object context)
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
            public T CreateCanvas<T>(object context = null, params object[] args) where T : Canvas
            {
                var constructorArgs = new List<object>() { TypeO, this, context };
                constructorArgs.AddRange(args);
                var canvas = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
                return canvas;
            }

            public T CreateCanvas<T>(Rectangle rect, object context = null, params object[] args) where T : Canvas
            {
                var constructorArgs = new List<object>() { TypeO, this, context };
                constructorArgs.AddRange(args);
                var canvas = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
                if (rect != null)
                    canvas.Viewport = rect;
                return canvas;
            }
        }
    }
}
