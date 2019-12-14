using System;
using System.Collections.Generic;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;

namespace Typedeaf.TypeOCore
{
    namespace Content
    {
        public abstract class Font
        {
            /// <summary>
            /// Do not call directly, use Game.Content.LoadTexture instead
            /// </summary>
            public Font(string path)
            {
                FileName = path;
            }

            public string FileName { get; protected set; }
            public abstract Vec2 MeasureString(string text);
        }

        public abstract partial class ContentLoader
        {
            public T LoadFont<T>(string path, params object[] args) where T : Font
            {
                var constructorArgs = new List<object>() { path };
                constructorArgs.AddRange(args);
                var font = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
                return font;
            }
        }
    }

    namespace Graphics
    {
        public abstract partial class Canvas
        {
            public abstract void DrawText(Font font, string text, Vec2 pos);
            public abstract void DrawText(Font font, string text, Vec2 pos, Vec2 scale, double rotate, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source);
            public abstract void DrawText(Font font, string text, Vec2 pos, Vec2? scale = null, double rotate = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null);
        }
    }
}