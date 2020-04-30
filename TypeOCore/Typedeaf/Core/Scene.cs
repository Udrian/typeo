using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    public abstract class Scene : IHasEntities, IHasContext
    {
        Context IHasContext.Context { get; set; }
        private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }
        protected ILogger Logger { get; set; }

        public SceneList Scenes { get; set; }
        public Window Window { get; set; }
        public Canvas Canvas { get; set; }
        public ContentLoader ContentLoader { get; set; }
        public EntityList Entities { get; set; }

        public bool IsInitialized { get; set; } = false;
        public bool Pause         { get; set; } = false;
        public bool Hide          { get; set; } = false;

        protected Scene() { }

        public abstract void Initialize();
        public abstract void Update(double dt);
        public abstract void Draw();

        public abstract void OnExit(Scene to);
        public abstract void OnEnter(Scene from);

        public D CreateDrawable<D>() where D : Drawable, new() //TODO: Duplicate code from Entity, look over this
        {
            Logger.Log(LogLevel.Ludacris, $"Creating Drawable of type '{typeof(D).FullName}' into {this.GetType().FullName}");

            var drawable = new D();

            Context.InitializeObject(drawable, this);
            drawable.Initialize();

            return drawable;
        }

        public L CreateLogic<L>() where L : Logic, new() //TODO: Duplicate code from Entity, look over this
        {
            Logger.Log(LogLevel.Ludacris, $"Creating Logic of type '{typeof(L).FullName}' into {this.GetType().FullName}");

            var logic = new L();

            Context.InitializeObject(logic, this);
            logic.Initialize(); 

            return logic;
        }
    }
}
