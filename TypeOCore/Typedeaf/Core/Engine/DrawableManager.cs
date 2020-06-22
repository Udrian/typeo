using System.Collections.Generic;
using System.Linq;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;

namespace TypeOEngine.Typedeaf.Core.Engine
{
    public class DrawableManager<T> : IHasContext where T : Drawable
    {
        Context IHasContext.Context { get; set; }
        private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }

        internal List<T> Drawables { get; private set; }
        private DrawStack DrawStack { get; set; }
        private object Parent { get; set; }

        internal DrawableManager(DrawStack drawStack, object parent)
        {
            Drawables = new List<T>();
            DrawStack = drawStack;
            Parent = parent;
        }

        public D CreateDrawable<D>(bool pushToDrawStack = true) where D : T, new() //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
        {
            var drawable = Context.CreateDrawable<D>(Parent, pushToDrawStack ? DrawStack : null);
            Drawables.Add(drawable);

            return drawable;
        }

        public int DestroyDrawable<D>() where D : T //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
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

        public void DestroyDrawable(T drawable) //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
        {
            Context.DestroyDrawable(drawable, DrawStack);
            Drawables.Remove(drawable);
        }

        public IEnumerable<D> GetDrawables<D>() where D : T //TODO: Maybe have all the Create, Destroy and Get logic in Handler classes in a "Node" class instead?
        {
            return Drawables.FindAll(drawable => drawable is D).Cast<D>();
        }
    }
}
