using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Graphics;

namespace Typedeaf.TypeO.Engine
{
    namespace Content
    {
        public abstract class Texture
        {
            public string FileName { get; protected set; }
            private Core.TypeO TypeO { get; set; }
            /// <summary>
            /// Do not call directly, use Game.CreateWindow instead
            /// </summary>
            public Texture(Core.TypeO typeO, string path)
            {
                TypeO = typeO;
                FileName = path;
            }
        }

        public abstract partial class ContentLoader
        {
            public abstract Texture LoadTexture(string path);
        }
    }
}
