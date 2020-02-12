using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Interfaces
    {
        public interface IHasLogic
        {
            public bool PauseLogic { get; set; }
            public Logic Logic { get; set; }

            public void CreateLogic(Entity entity);
        }

        public interface IHasLogic<L> : IHasLogic where L : Logic, new()
        {
            Logic IHasLogic.Logic { get { return Logic; } set { Logic = value as L; } }
            public new L Logic { get; set; }

            void IHasLogic.CreateLogic(Entity entity)
            {
                Logic = new L();

                if (Logic is IHasEntity)
                {
                    (Logic as IHasEntity).Entity = entity;
                }

                if (Logic is IHasData)
                {
                    (Logic as IHasData).EntityData = (entity as IHasData)?.EntityData;
                }
            }
        }
    }
}
