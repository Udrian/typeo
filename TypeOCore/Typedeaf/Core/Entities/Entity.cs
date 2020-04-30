using System.Collections.Generic;
using System.Linq;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity : IHasContext
        {
            Context IHasContext.Context { get; set; }
            private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }
            protected ILogger Logger { get; set; }

            public string ID { get; internal set; }
            public Entity Parent { get; internal set; }
            internal EntityList ParentEntityList { get; set; } //TODO: This should change to something else

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
                WillBeDeleted = true;
            }
            public bool WillBeDeleted { get; private set; }

            public L CreateLogic<L>() where L : Logic, new()
            {
                Logger.Log(LogLevel.Ludacris, $"Creating Logic of type '{typeof(L).FullName}' into {this.GetType().FullName}");

                var logic = new L()
                {
                    Parent = this
                };

                Context.InitializeObject(logic, this);
                logic.Initialize();

                ParentEntityList.AddUpdatable(logic); //TODO: Not like this, should be sent back and then be added by caller
                Logics.Add(logic);

                return logic;
            }

            public bool RemoveLogic<L>() where L : Logic
            {
                var logic = Logics.FirstOrDefault(logic => logic.GetType() == typeof(L));
                if(logic == null) return false;
                Logics.Remove(logic);
                ParentEntityList.RemoveUpdatable(logic); //TODO: Not really like this?
                return true;
            }

            public L GetLogic<L>() where L : Logic
            {
                return Logics.FirstOrDefault(logic => logic is L) as L;
            }

            public D CreateDrawable<D>() where D : Drawable, new()
            {
                Logger.Log(LogLevel.Ludacris, $"Creating Drawable of type '{typeof(D).FullName}' into {this.GetType().FullName}");

                var drawable = new D
                {
                    Entity = this
                };

                Context.InitializeObject(drawable, this);
                drawable.Initialize();

                ParentEntityList.AddDrawable(drawable); //TODO: Not like this, should be sent back and then be added by caller
                Drawables.Add(drawable);

                return drawable;
            }

            public bool RemoveDrawable<D>() where D : Drawable
            {
                var drawable = Drawables.FirstOrDefault(drawable => drawable.GetType() == typeof(D));
                if(drawable == null) return false;
                Drawables.Remove(drawable);
                ParentEntityList.RemoveDrawable(drawable); //TODO: Not really like this?
                return true;
            }

            public D GetDrawable<D>() where D : Drawable
            {
                return Drawables.FirstOrDefault(drawable => drawable is D) as D;
            }
        }
    }
}