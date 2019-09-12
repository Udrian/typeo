using System.Threading.Tasks;

namespace Typedeaf.TypeO.Engine.Core
{
    public partial class TypeO
    {
        protected InternalGame Context { get; set; }

        private TypeO() { }
        public static TypeO Create(Game game)
        {
            var typeO = new TypeO();
            typeO.Context = new InternalGame(game);
            return typeO;
        }

        public TypeO LoadModule(TypeO.Module module)
        {
            module.Init(this);
            return this;
        }

        public async Task Start()
        {
            float dt = 0;
            while (!Context.Game.Exit)
            {
                dt++;
                await Context.Update(dt);
                await Context.Draw();
            }
        }
    }
}
