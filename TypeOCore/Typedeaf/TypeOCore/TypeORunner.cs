using System;

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

            public TypeO.Runner<T> SetKeyAlias(object input, object key)
            {
                TypeO.KeyConverter.SetKeyAlias(input, key);
                return this;
            }

            public void Start()
            {
                //Initialize the game
                var game = (T)Activator.CreateInstance(typeof(T));
                TypeO.Game = game;
                (game as IHasTypeO).SetTypeO(TypeO);
                game.Initialize();

                TypeO.Start();

                //Cleanup
                foreach(var module in TypeO.Modules)
                {
                    module.Cleanup();
                }
            }
        }

        public static TypeO.Runner<T> Create<T>() where T : Game
        {
            var runner = new TypeO.Runner<T>();
            return runner;
        }
    }
}
