using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Graphics;
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
        public class Interfaces
        {
            public interface ISingleCanvasGame
            {
                public void SetWindow(Window window);
                public void SetCanvas(Canvas canvas);
            }

            public interface ISingleCanvasGame<W, C> : ISingleCanvasGame where W : Window where C : Canvas
            {
                void ISingleCanvasGame.SetWindow(Window window)
                {
                    Window = (W)window;
                }

                void ISingleCanvasGame.SetCanvas(Canvas canvas)
                {
                    Canvas = (C)canvas;
                }

                public W Window { get; set; }
                public C Canvas { get; set; }
            }
        }

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
