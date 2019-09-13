using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Typedeaf.TypeO.Engine.Core
{
    public partial class TypeO
    {
        public partial class Runner<T> where T : Game
        {
            protected TypeO TypeO { get; private set; }

            public Runner()
            {
                TypeO = new TypeO();
            }

            public void Start()
            {
                //Initialize the game
                var game = (T)Activator.CreateInstance(typeof(T), TypeO);
                TypeO.Game = game;
                game.Init();

                TypeO.Start();

                //Cleanup
                foreach(var module in TypeO.Modules)
                {
                    module.Cleanup();
                }
            }
        }

        private TypeO() {
            Modules = new List<Module>();
            Canvas = new List<Graphics.Canvas>();
            LastTick = DateTime.UtcNow;
        }

        public static TypeO.Runner<T> Create<T>() where T : Game
        {
            var factory = new TypeO.Runner<T>();
            return factory;
        }

        private DateTime LastTick { get; set; }
        public void Start()
        {
            while (!Game.Exit)
            {
                var dt = (DateTime.UtcNow - LastTick).TotalSeconds;
                LastTick = DateTime.UtcNow;

                foreach (var module in Modules)
                {
                    module.Update((float)dt);
                }

                Game.Update((float)dt);
                foreach (var canvas in Canvas)
                {
                    Game.Draw(canvas);
                }
            }
        }
    }
}
