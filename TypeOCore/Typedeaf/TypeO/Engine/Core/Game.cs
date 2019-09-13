using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Graphics;

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
        public abstract void Update(float dt);
        public abstract void Draw(Canvas canvas);
        public bool Exit { get; set; } = false;
    }

    public partial class TypeO
    {
        public Game Game { get; set; }
    }
}
