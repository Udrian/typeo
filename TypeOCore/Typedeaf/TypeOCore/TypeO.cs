using System;
using System.Collections.Generic;
using Typedeaf.TypeOCore.Input;

namespace Typedeaf.TypeOCore
{
    internal interface IHasTypeO
    {
        protected TypeO TypeO { get; set; }
        public void SetTypeO(TypeO typeO)
        {
            TypeO = typeO;
        }
        public TypeO GetTypeO()
        {
            return TypeO;
        }
    }

    public partial class TypeO
    {
        private TypeO() {
            Modules = new List<Module>();
            KeyConverter = new KeyConverter();
            LastTick = DateTime.UtcNow;
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
