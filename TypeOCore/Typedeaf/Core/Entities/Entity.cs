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

            public Entity() { }

            public abstract void Initialize();
            public abstract void Cleanup();

            public void Remove()
            {
                WillBeDeleted = true;
            }
            public bool WillBeDeleted { get; private set; }

            public D CreateDrawable<D>() where D : Drawable, new()
            {
                Logger.Log(LogLevel.Ludacris, $"Creating Drawable of type '{typeof(D).FullName}' into {this.GetType().FullName}");

                var drawable = new D
                {
                    Entity = this
                };

                Context.InitializeObject(drawable, this);
                drawable.Initialize();

                ParentEntityList.AddDrawable(drawable); //TODO: Not like this

                return drawable;
            }
        }
    }
}