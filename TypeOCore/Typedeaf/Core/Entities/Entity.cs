using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities
    {
        public abstract class Entity : IHasTypeO
        {
            TypeO IHasTypeO.TypeO { get; set; }
            private TypeO TypeO { get => (this as IHasTypeO).TypeO; set => (this as IHasTypeO).TypeO = value; }

            public Entity Parent { get; set; }

            protected List<Logic> Logics { get; set; }
            public List<Logic> GetLogics()
            {
                return Logics;
            }

            protected Entity()
            {
                Logics = new List<Logic>();
            }

            public abstract void Initialize();

            public L CreateLogic<L>() where L : Logic, new()
            {
                var logic = TypeO.CreateLogic<L>(this);

                if (logic is IHasEntity)
                {
                    (logic as IHasEntity).Entity = this;
                }
                if (logic is IHasScene)
                {
                    (logic as IHasScene).Scene = (this as IHasScene)?.Scene;
                }
                if (logic is IHasData)
                {
                    (logic as IHasData).EntityData = (this as IHasData)?.EntityData;
                }

                logic.Initialize();

                Logics.Add(logic);
                return logic;
            }

            public void Remove()
            {
                WillBeDeleted = true;
            }
            public bool WillBeDeleted { get; private set; }
        }
    }
}