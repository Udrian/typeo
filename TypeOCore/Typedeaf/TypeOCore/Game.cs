using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    public interface IHasGame<G> where G : Game
    {
        public G Game { get; set; }
    }

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
