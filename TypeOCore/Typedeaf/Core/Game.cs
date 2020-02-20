using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    public abstract class Game : IHasContext
    {
        Context IHasContext.Context { get; set; }
        private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }

        public string Name { get { return Context.Name; } }

        protected Game() {}

        public SceneList CreateScenes()
        {
            var scenes = new SceneList();
            Context.InitializeObject(scenes);
            return scenes;
        }

        public abstract void Initialize();
        public abstract void Update(double dt);
        public abstract void Draw();
        public abstract void Cleanup();
        public void Exit() { Context.Exit(); }
    }
}