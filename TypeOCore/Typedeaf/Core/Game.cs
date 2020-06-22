using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeOEngine.Typedeaf.Core
{
    public abstract class Game : IHasContext
    {
        Context IHasContext.Context { get; set; }
        private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }

        public string Name { get { return Context.Name; } }

        public DrawableManager<Drawable> Drawables { get; private set; }

        protected Game() { }

        public SceneList CreateSceneHandler()
        {
            var scenes = new SceneList();
            Context.InitializeObject(scenes);
            return scenes;
        }

        internal void InternalInitialize()
        {
            Drawables = new DrawableManager<Drawable>(null, this);
            Context.InitializeObject(Drawables, this);
        }

        public abstract void Initialize();
        public abstract void Update(double dt);
        public abstract void Draw();
        public abstract void Cleanup();
        public void Exit() { Context.Exit(); }

        public L CreateLogic<L>() where L : Logic, new()
        {
            return Context.CreateLogic<L>(this, null);
        }

        public void DestroyLogic(Logic logic)
        {
            Context.DestroyLogic(logic, null);
        }
    }
}