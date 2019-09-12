using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Graphics;

namespace Typedeaf.TypeO.Engine
{
    namespace Graphics
    {
        public abstract class Window
        {
            /// <summary>
            /// Do not call directly, use Game.CreateWindow instead
            /// </summary>
            public Window(string title,
                          Vec2 position,
                          Vec2 size,
                          bool fullscreen,
                          bool borderless)
            {
                Title      = title;
                Position   = position;
                Size       = size;
                Fullscreen = fullscreen;
                Borderless = borderless;
            }

            public virtual string Title      { get; set; }
            public virtual Vec2   Position   { get; set; }
            public virtual Vec2   Size       { get; set; }
            public virtual bool   Fullscreen { get; set; }
            public virtual bool   Borderless { get; set; }
        }
    }

    namespace Core
    {
        public abstract partial class Game
        {
            public Window CreateWindow(string title,
                                         Vec2 position,
                                         Vec2 size,
                                         bool fullscreen = false,
                                         bool borderless = false)
            {
                return TypeO.CreateWindow?.Invoke(title, position, size, fullscreen, borderless);
            }
        }

        public delegate Window CreateWindowDelegate(string title,
                                                      Vec2 position,
                                                      Vec2 size,
                                                      bool fullscreen,
                                                      bool borderless);
        public partial class TypeO
        {
            public CreateWindowDelegate CreateWindow;
        }
    }
}
