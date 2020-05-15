using System.Collections.Generic;
using System.Linq;
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

        public SceneList Scenes { get; set; }
        public Window Window { get; set; }
        public Canvas Canvas { get; set; }
        public ContentLoader ContentLoader { get; set; }
        public EntityList Entities { get; set; } //TODO: Look over this
        public DrawStack DrawStack { get; private set; } //TODO: Should be able to create draw stack from Game maybe?
        private List<Logic> Logics { get; set; }
        private List<Drawable> Drawables { get; set; }

        public bool IsInitialized { get; set; } = false;
        public bool Pause         { get; set; } = false;
        public bool Hide          { get; set; } = false;

        protected Scene()
        {
            DrawStack = new DrawStack();
            Logics = new List<Logic>();
            Drawables = new List<Drawable>();
        }

        public abstract void Initialize();
        public abstract void Update(double dt);
        public abstract void Draw();

        public abstract void OnExit(Scene to);
        public abstract void OnEnter(Scene from);

        public D CreateDrawable<D>(bool pushToDrawStack = true) where D : Drawable, new()
        {
            var drawable = Context.CreateDrawable<D>(this, pushToDrawStack ? DrawStack : null);
            Drawables.Add(drawable);

            return drawable;
        }

        public int DestroyDrawable<D>() where D : Drawable
        {
            var destroyCount = 0;
            foreach(var drawable in Drawables)
            {
                if(drawable is D)
                {
                    DestroyDrawable(drawable);
                    destroyCount++;
                }
            }

            return destroyCount;
        }

        public void DestroyDrawable(Drawable drawable)
        {
            Context.DestroyDrawable(drawable, DrawStack);
            Drawables.Remove(drawable);
        }

        public IEnumerable<D> GetDrawable<D>() where D : Drawable
        {
            return Drawables.FindAll(drawable => drawable is D).Cast<D>();
        }

        public L CreateLogic<L>() where L : Logic, new()
        {
            var logic = Context.CreateLogic<L>(this, Entities);
            Logics.Add(logic);
            return logic;
        }

        public int DestroyLogic<L>() where L : Logic
        {
            var destroyCount = 0;
            foreach(var logic in Logics)
            {
                if(logic is L)
                {
                    DestroyLogic(logic);
                    destroyCount++;
                }
            }

            return destroyCount;
        }

        public void DestroyLogic(Logic logic)
        {
            Context.DestroyLogic(logic, Entities);
            Logics.Remove(logic);
        }

        public IEnumerable<L> GetLogics<L>() where L : Logic
        {
            return Logics.FindAll(logic => logic is L).Cast<L>();
        }
    }
}
