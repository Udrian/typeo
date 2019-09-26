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
        public T CreateWindow<T>(params object[] args) where T : Window
        {
            var constructorArgs = new List<object>() { TypeO };
            constructorArgs.AddRange(args);
            var win = (T)Activator.CreateInstance(typeof(T), constructorArgs.ToArray());
            return win;
        }
    }
}
