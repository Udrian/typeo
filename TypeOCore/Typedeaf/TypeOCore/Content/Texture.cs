using System;
using System.Collections.Generic;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;

namespace Typedeaf.TypeOCore
{
    namespace Content
    {
        public abstract class Texture
        {
            public enum Flipped
            {
                None,
                Horizontal,
                Vertical,
                Both
            }

            /// <summary>
            /// Do not call directly, use Game.Content.LoadTexture instead
            /// </summary>
            public Texture( string path)
            {
                FilePath = path;
            }

            public string FilePath { get; protected set; }
            public Vec2   Size     { get; protected set; }
        }

        public partial class ContentLoader {

            public T LoadTexture<T>(string path, params object[] args) where T : Texture
            {
                var constructorArgs = new List<object>() { path };
                constructorArgs.AddRange(args);
                var texture = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
                return texture;
            }
        }
    }

    namespace Graphics
    {
        public abstract partial class Canvas
        {
            public abstract void DrawImage(Texture texture, Vec2 pos);
            public abstract void DrawImage(Texture texture, Vec2 pos, Vec2 scale, double rotation, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source);
            public abstract void DrawImage(Texture texture, Vec2 pos, Vec2? scale = null, double rotation = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null);
        }
    }
}