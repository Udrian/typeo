using System;
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

            public async Task Start()
            {
                //Initialize the game
                var game = (T)Activator.CreateInstance(typeof(T), TypeO);
                TypeO.Game = game;
                game.Init();

                float dt = 0;
                while (!TypeO.Game.Exit)
                {
                    dt++;
                    await TypeO.Game.Update(dt);
                    await TypeO.Game.Draw();
                }
            }
        }

        private TypeO() { }

        public static TypeO.Runner<T> Create<T>() where T : Game
        {
            var factory = new TypeO.Runner<T>();
            return factory;
        }
    }
}
