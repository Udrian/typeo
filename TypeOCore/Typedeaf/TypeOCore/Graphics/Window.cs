using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Graphics;

namespace Typedeaf.TypeOCore
{
    namespace Graphics
    {
        public abstract partial class Window
        {
            protected TypeO TypeO { get; set; }
            /// <summary>
            /// Do not call directly, use Game.CreateWindow instead
            /// </summary>
            public Window(TypeO typeO)
            {
                TypeO = typeO;
            }

            public virtual string Title      { get; set; }
            public virtual Vec2   Position   { get; set; }
            public virtual Vec2   Size       { get; set; }
            public virtual bool   Fullscreen { get; set; }
            public virtual bool   Borderless { get; set; }
        }
    }

    public abstract partial class Game
    {
        public Window CreateWindow(string title,
                                        Vec2 position,
                                        Vec2 size,
                                        bool fullscreen = false,
                                        bool borderless = false)
        {
            return TypeO.CreateWindow?.Invoke(TypeO, title, position, size, fullscreen, borderless);
        }
    }

        
    public partial class TypeO
    {
        public delegate Window CreateWindowDelegate(TypeO typeO,
                                                string title,
                                                    Vec2 position,
                                                    Vec2 size,
                                                    bool fullscreen,
                                                    bool borderless);
        public CreateWindowDelegate CreateWindow;
    }
}
