using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Entities;

namespace Typedeaf.TypeOCore
{
    public interface IHasTypeO
    {
        protected ITypeO TypeO { get; set; }
        public void SetTypeO(ITypeO typeO)
        {
            TypeO = typeO;
        }
        public ITypeO GetTypeO()
        {
            return TypeO;
        }
    }

    public interface ITypeO
    {
        public void Start();
        public void Exit();

        public ITypeO AddService<I, S>() where I : class where S : Service, new();
        public M LoadModule<M>() where M : Module, new();
    }

    public class TypeO : ITypeO
    {
        public static ITypeO Create<G>() where G : Game, new()
        {
            var typeO = new TypeO
            {
                Game = new G()
            };
            (typeO.Game as IHasTypeO).SetTypeO(typeO);
            return typeO;
        }

        public Game Game { get; set; }
        private DateTime LastTick { get; set; }
        private List<Module> Modules { get; set; }

        public TypeO() : base()
        {
            LastTick = DateTime.UtcNow;
            Modules = new List<Module>();
        }

        private bool ExitApplication = false;
        public void Exit()
        {
            ExitApplication = true;
        }

        public ITypeO AddService<I, S>() where I : class where S : Service, new()
        {
            Game.AddService<I, S>();
            return this;
        }

        public M LoadModule<M>() where M : Module, new()
        {
            var module = new M();
            (module as IHasTypeO).SetTypeO(this);
            if(module is IHasGame)
            {
                (module as IHasGame).SetGame(Game);
            }
            module.Initialize();
            Modules.Add(module);

            return module;
        }

        public void Start()
        {
            //Initialize the game
            Game.Initialize();

            while (!ExitApplication)
            {
                var dt = (DateTime.UtcNow - LastTick).TotalSeconds;
                LastTick = DateTime.UtcNow;

                foreach (var module in Modules)
                {
                    (module as IIsUpdatable)?.Update(dt);
                }

                foreach (var service in Game.GetServices())
                {
                    (service as IIsUpdatable)?.Update(dt);
                }

                Game.Update(dt);
                Game.Draw();
            }

            //Cleanup
            foreach (var module in Modules)
            {
                module.Cleanup();
            }
        }
    }
}
