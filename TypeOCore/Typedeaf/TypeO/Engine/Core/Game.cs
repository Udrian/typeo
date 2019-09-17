using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Graphics;

namespace Typedeaf.TypeO.Engine.Core
{
    public abstract partial class Game
    {
        private TypeO TypeO { get; set; }
        public Game(TypeO typeO)
        {
            TypeO = typeO;
            //Content = TypeO?.CreateContentLoader();
        }
        protected Game() { }
        public abstract void Initialize();
        public abstract void Update(float dt);
        public abstract void Draw(Canvas canvas);
        public bool Exit { get; set; } = false;
    }

    public partial class TypeO
    {
        public Game Game { get; set; }
    }
}
