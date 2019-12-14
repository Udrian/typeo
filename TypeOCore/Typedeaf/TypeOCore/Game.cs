using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    public interface IHasGame
    {
        public void SetGame(Game game);
    }

    public interface IHasGame<G> : IHasGame where G : Game
    {
        public G Game { get; set; }

        void IHasGame.SetGame(Game game)
        {
            Game = (G)game;
        }
    }

    public abstract partial class Game : IHasTypeO
    {
        ITypeO IHasTypeO.TypeO { get; set; }
        private ITypeO TypeO { get { return (this as IHasTypeO).GetTypeO(); } }

        public Game()
        {
            Services = new Dictionary<Type, Service>();
            Input = new InputHandler(this);
        }
        public abstract void Initialize();
        public abstract void Update(float dt);
        public abstract void Draw();
        public void Exit() { TypeO.Exit(); }
    }
}
