using TypeOEngine.Typedeaf.Core.Collections;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public class UpdateLoop
        {
            private DelayedList<IIsUpdatable> Updatables { get; set; }

            internal UpdateLoop()
            {
                Updatables = new DelayedList<IIsUpdatable>();
            }

            public void Update(double dt)
            {
                foreach(var updatable in Updatables)
                {
                    //if(entity.WillBeDeleted) continue; //TODO: Look over this

                    if(!updatable.Pause)
                    {
                        updatable.Update(dt);
                    }
                }

                Updatables.Process();
            }

            public void Push(IIsUpdatable updatable)
            {
                if(updatable == null) return;
                Updatables.Add(updatable);
            }

            public void Pop(IIsUpdatable updatable)
            {
                if(updatable == null) return;
                Updatables.Remove(updatable);
            }
        }
    }
}
