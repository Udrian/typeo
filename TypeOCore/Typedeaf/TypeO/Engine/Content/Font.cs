using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Graphics;

namespace Typedeaf.TypeO.Engine
{
    namespace Content
    {
        public abstract class Font
        {
            private Core.TypeO TypeO { get; set; }
            /// <summary>
            /// Do not call directly, use Game.Content.LoadTexture instead
            /// </summary>
            public Font(Core.TypeO typeO, string path, int fontSize)
            {
                TypeO = typeO;
                FileName = path;
                FontSize = fontSize;
            }

            public int FontSize { get; protected set; }
            public string FileName { get; protected set; }
            public abstract Vec2 MeasureString(string text);
        }
    }

    namespace Graphics
    {
        public abstract partial class Canvas
        {
            public abstract Font LoadFont(string path, int fontSize);

            public abstract void DrawText(Font font, string text, Vec2 pos);
            public abstract void DrawText(Font font, string text, Vec2 pos, Vec2 scale, double rotate, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source);
            public abstract void DrawText(Font font, string text, Vec2 pos, Vec2? scale = null, double rotate = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null);
        }
    }
}