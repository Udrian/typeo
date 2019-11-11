using System;
using System.Collections.Generic;
using Typedeaf.TypeOCommon;

namespace Typedeaf.TypeOCore
{
    namespace Graphics
    {
        public abstract partial class Canvas : IHasTypeO
        {
            TypeO IHasTypeO.TypeO { get; set; }
            protected TypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

            protected Window Window { get; set; }
            protected Game Game { get; set; }

            /// <summary>
            /// Do not call directly, use Window.CreateCanvas instead
            /// </summary>
            public Canvas(Game game, Window window)
            {
                Window = window;
                Game = game;
                Scenes = new Dictionary<Type, Scene>();
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
            public T CreateCanvas<T>(params object[] args) where T : Canvas
            {
                var constructorArgs = new List<object>() { Game, this };
                constructorArgs.AddRange(args);
                var canvas = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
                (canvas as IHasTypeO).SetTypeO(TypeO);
                return canvas;
            }

            public T CreateCanvas<T>(Rectangle rect, params object[] args) where T : Canvas
            {
                var constructorArgs = new List<object>() { this };
                constructorArgs.AddRange(args);
                var canvas = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
                (canvas as IHasTypeO).SetTypeO(TypeO);
                if (rect != null)
                    canvas.Viewport = rect;
                return canvas;
            }
        }
    }
}
