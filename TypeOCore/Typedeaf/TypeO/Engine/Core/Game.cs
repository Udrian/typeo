using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;

namespace Typedeaf.TypeO.Engine.Core
{
    public abstract partial class Game
    {
        protected Game() {}
        public abstract void Init();
        public abstract Task Update(float dt);
        public abstract Task Draw();
        public bool Exit { get; set; } = false;
    }

    public partial class TypeO
    {
        protected class InternalGame
        {
            public Game Game { get; set; }

            public InternalGame(Game game)
            {
                Game = game;
                Game.Init();
            }
            public async Task Update(float dt)
            {
                await Game.Update(dt);
            }

            public async Task Draw()
            {
                await Game.Draw();
            }
        }
    }
}
