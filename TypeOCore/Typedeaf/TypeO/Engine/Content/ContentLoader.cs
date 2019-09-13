using System.Collections.Generic;
using Typedeaf.TypeO.Engine.Content;

namespace Typedeaf.TypeO.Engine
{
    namespace Content
    {
        public abstract partial class ContentLoader
        {
            private Core.TypeO TypeO { get; set; }

            public ContentLoader(Core.TypeO typeO)
            {
                TypeO = typeO;
            }

        }
    }

    namespace Core
    {
        public partial class TypeO
        {
            public delegate ContentLoader CreateContentLoaderDelegate();
            public CreateContentLoaderDelegate CreateContentLoader;
        }
        public abstract partial class Game
        {
            public ContentLoader Content { get; set; }
        }
    }
}
