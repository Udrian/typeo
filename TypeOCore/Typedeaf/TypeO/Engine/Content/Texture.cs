﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Graphics;

namespace Typedeaf.TypeO.Engine
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

            private Core.TypeO TypeO { get; set; }
            /// <summary>
            /// Do not call directly, use Game.Content.LoadTexture instead
            /// </summary>
            public Texture(Core.TypeO typeO, string path)
            {
                TypeO = typeO;
                FileName = path;
            }


            public string FileName { get; protected set; }

            public abstract Vec2 Size { get; protected set; }

            /*public abstract void SetColor(Vec2 pos, Color color);
            public abstract Color GetColor(Vec2 pos);

            public abstract ColorMap ColorMap { get; set; }
            public abstract Texture Cut(Rectangle rectangle);*/
        }
    }

    namespace Graphics
    {
        public abstract partial class Canvas
        {
            public abstract Texture LoadTexture(string path);

            public abstract void DrawImage(Texture texture, Vec2 pos);
            public abstract void DrawImage(Texture texture, Vec2 pos, Vec2 scale, double rotate, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source);
            public abstract void DrawImage(Texture texture, Vec2 pos, Vec2? scale = null, double rotate = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null);
        }
    }
}