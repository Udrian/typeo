using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;

namespace Typedeaf.TypeO.Engine.Core
{
    public abstract partial class Game
    {
        private TypeO TypeO { get; set; }
        public Game(TypeO typeO)
        {
            TypeO = typeO;
        }
        protected Game() { }
        public abstract void Init();
        public abstract Task Update(float dt);
        public abstract Task Draw();
        public bool Exit { get; set; } = false;
    }

    public partial class TypeO
    {
        public Game Game { get; set; }
    }
}
