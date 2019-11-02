using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    public interface IHasGame<G> where G : Game
    {
        public G Game { get; set; }
    }

    public abstract partial class Game : IHasTypeO
    {
        TypeO IHasTypeO.TypeO { get; set; }
        private TypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

        public Game()
        {
            Services = new Dictionary<Type, Service>();
            Input = new InputHandler(this);
        }
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
