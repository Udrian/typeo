using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Graphics;
using Typedeaf.TypeO.Engine.Input;

namespace Typedeaf.TypeO.Engine.Core
{
    public abstract partial class Game
    {
        private TypeO TypeO { get; set; }
        public Game(TypeO typeO)
        {
            TypeO = typeO;
            Services = new Dictionary<Type, Service>();
            Input = new InputHandler(typeO);
        }
        protected Game() { }
        public abstract void Initialize();
        public abstract void Update(float dt);
        public abstract void Draw();
        public void Exit() { TypeO.Exit = true; }
    }

    public partial class TypeO
    {
        public Game Game { get; set; }
    }
}
