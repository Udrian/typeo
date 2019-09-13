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
            protected Core.TypeO  TypeO  { get; set; }
            protected      Window Window { get; set; }

            /// <summary>
            /// Do not call directly, use Window.CreateCanvas instead
            /// </summary>
            public Canvas(Core.TypeO typeO, Window window)
            {
                TypeO  = typeO;
                Window = window;
            }

            public abstract void Clear();
            public abstract void DrawLine();
            public abstract void Present();
        }

        public abstract partial class Window
        {
            public Canvas CreateCanvas()
            {
                var canvas = TypeO.CreateCanvas?.Invoke(TypeO, this);
                TypeO.AddCanvas(canvas);
                return canvas;
            }
        }
    }

    namespace Core
    {
        public partial class TypeO
        {
            public delegate Canvas CreateCanvasDelegate(TypeO typeO, Window window);
            public CreateCanvasDelegate CreateCanvas;
            private List<Canvas> Canvas;
            public void AddCanvas(Canvas canvas)
            {
                Canvas.Add(canvas);
            }
        }
    }
}
