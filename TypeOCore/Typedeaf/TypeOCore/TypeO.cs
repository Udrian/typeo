using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
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

            public TypeO.Runner<T> SetKey(object input, object key)
            {
                TypeO.KeyConverter.SetKey(input, key);
                return this;
            }

            public void Start()
            {
                //Initialize the game
                var game = (T)Activator.CreateInstance(typeof(T), TypeO);
                TypeO.Game = game;
                game.Initialize();

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
            KeyConverter = new KeyConverter(this);
            LastTick = DateTime.UtcNow;
        }

        public static TypeO.Runner<T> Create<T>() where T : Game
        {
            var factory = new TypeO.Runner<T>();
            return factory;
        }

        public bool Exit { get; set; } = false;
        private DateTime LastTick { get; set; }
        public void Start()
        {
            while (!Exit)
            {
                var dt = (float)(DateTime.UtcNow - LastTick).TotalSeconds;
                LastTick = DateTime.UtcNow;

                foreach (var module in Modules)
                {
                    module.Update(dt);
                }

                foreach(var service in Game.GetServices())
                {
                    service.Update(dt);
                }

                Game.Update(dt);
                Game.Draw();
            }
        }
    }
}
