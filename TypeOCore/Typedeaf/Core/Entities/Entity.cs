using System.Collections.Generic;
using System.Linq;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

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
            public UpdateLoop UpdateLoop { get; internal set; }

            private List<Logic> Logics { get; set; } //TODO: Do we want internally saved Logic list?
            private List<Drawable> Drawables { get; set; } //TODO: Do we want internally saved Drawable list?

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
                foreach(var logic in Logics)
                {
                    UpdateLoop.Pop(logic);
                }
                UpdateLoop.Pop(this as IIsUpdatable);
                WillBeDeleted = true;
            }
            public bool WillBeDeleted { get; private set; }

            public D CreateDrawable<D>(bool pushToDrawStack = true) where D : Drawable, new() //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
            {
                var drawable = Context.CreateDrawable<D>(this, pushToDrawStack ? DrawStack : null);
                Drawables.Add(drawable);

                return drawable;
            }

            public int DestroyDrawable<D>() where D : Drawable //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
            {
                var destroyCount = 0;
                foreach(var drawable in Drawables)
                {
                    if(drawable is D)
                    {
                        Context.DestroyDrawable(drawable, DrawStack);
                        destroyCount++;
                    }
                }

                Drawables.RemoveAll(drawable => drawable is D);

                return destroyCount;
            }

            public void DestroyDrawable(Drawable drawable) //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
            {
                Context.DestroyDrawable(drawable, DrawStack);
                Drawables.Remove(drawable);
            }

            public IEnumerable<D> GetDrawables<D>() where D : Drawable //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
            {
                return Drawables.FindAll(drawable => drawable is D).Cast<D>();
            }

            public L CreateLogic<L>(bool pushToUpdateLoop = true) where L : Logic, new() //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
            {
                var logic = Context.CreateLogic<L>(this, pushToUpdateLoop ? UpdateLoop : null);
                Logics.Add(logic);
                return logic;
            }

            public int DestroyLogic<L>() where L : Logic //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
            {
                var destroyCount = 0;
                foreach(var logic in Logics)
                {
                    if(logic is L)
                    {
                        Context.DestroyLogic(logic, UpdateLoop);
                        destroyCount++;
                    }
                }

                Logics.RemoveAll(logic => logic is L);

                return destroyCount;
            }

            public void DestroyLogic(Logic logic) //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
            {
                Context.DestroyLogic(logic, UpdateLoop);
                Logics.Remove(logic);
            }

            public IEnumerable<L> GetLogics<L>() where L : Logic //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
            {
                return Logics.FindAll(logic => logic is L).Cast<L>();
            }
        }
    }
}