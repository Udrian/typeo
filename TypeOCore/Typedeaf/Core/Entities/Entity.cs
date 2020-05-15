using System.Collections.Generic;
using System.Linq;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity : IHasContext
        {
            Context IHasContext.Context { get; set; }
            private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }

            public string ID { get; internal set; }
            public Entity Parent { get; internal set; }
            internal EntityList ParentEntityList { get; set; } //TODO: This should change to something else
            public DrawStack DrawStack { get; internal set; }

            private List<Logic> Logics { get; set; }
            private List<Drawable> Drawables { get; set; }

            public Entity()
            {
                Logics = new List<Logic>();
                Drawables = new List<Drawable>();
            }

            public abstract void Initialize();
            public abstract void Cleanup();

            public void Remove()
            {
                foreach(var drawable in Drawables)
                {
                    DrawStack.Pop(drawable);
                }
                DrawStack.Pop(this as IDrawable);
                WillBeDeleted = true;
            }
            public bool WillBeDeleted { get; private set; }

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

            public IEnumerable<D> GetDrawables<D>() where D : Drawable
            {
                return Drawables.FindAll(drawable => drawable is D).Cast<D>();
            }

            public L CreateLogic<L>() where L : Logic, new()
            {
                var logic = Context.CreateLogic<L>(this, ParentEntityList);
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
                Context.DestroyLogic(logic, ParentEntityList);
                Logics.Remove(logic);
            }

            public IEnumerable<L> GetLogics<L>() where L : Logic
            {
                return Logics.FindAll(logic => logic is L).Cast<L>();
            }
        }
    }
}