using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    internal interface IHasTypeO
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
        public KeyboardInput.Internal KeyHandler { get; set; }
        public KeyConverter KeyConverter { get; set; }

        public void Exit();

        public ITypeO SetKeyAlias(object input, object key);

        public M LoadModule<M>() where M : Module, new();

        public void Start();
    }

    public static class TypeO
    {
        public static ITypeO Create<G>() where G : Game, new()
        {
            var typeO = new TypeO<G>();
            return typeO;
        }
    }

    public class TypeO<G> : ITypeO where G : Game, new()
    {
        public KeyboardInput.Internal KeyHandler { get; set; }
        public KeyConverter KeyConverter { get; set; }

        public Game Game { get; set; }
        
        private DateTime LastTick { get; set; }

        protected List<Module> Modules;

        public TypeO() : base()
        {
            Modules = new List<Module>();
            KeyConverter = new KeyConverter();
            LastTick = DateTime.UtcNow;
        }

        private bool ExitApplication = false;
        public void Exit()
        {
            ExitApplication = true;
        }

        public ITypeO SetKeyAlias(object input, object key)
        {
            KeyConverter.SetKeyAlias(input, key);
            return this;
        }

        public M LoadModule<M>() where M : Module, new()
        {
            var module = new M();
            (module as IHasTypeO).SetTypeO(this);
            module.Initialize();
            Modules.Add(module);

            return module;
        }

        public void Start()
        {
            //Initialize the game
            Game = new G();
            (Game as IHasTypeO).SetTypeO(this);
            Game.Initialize();

            while (!ExitApplication)
            {
                var dt = (float)(DateTime.UtcNow - LastTick).TotalSeconds;
                LastTick = DateTime.UtcNow;

                foreach (var module in Modules)
                {
                    module.Update(dt);
                }

                foreach (var service in Game.GetServices())
                {
                    service.Update(dt);
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
