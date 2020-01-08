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

            public Window Window { get; set; }
            public Game   Game   { get; set; }

            protected Canvas()
            {
                Scenes = new Dictionary<Type, Scene>();
            }

            public abstract void Initialize();

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
            public C CreateCanvas<C>() where C : Canvas, new()
            {
                var canvas = new C();

                (canvas as IHasTypeO).SetTypeO(TypeO);
                canvas.Window = this;
                canvas.Game   = Game;
                if (Game is Game.Interfaces.ISingleCanvasGame)
                {
                    (Game as Game.Interfaces.ISingleCanvasGame).SetCanvas(canvas);
                }
                canvas.Initialize();
                return canvas;
            }

            public C CreateCanvas<C>(Rectangle rect) where C : Canvas, new()
            {
                var canvas = CreateCanvas<C>();

                if (rect != null)
                    canvas.Viewport = rect;
                return canvas;
            }
        }
    }
}
