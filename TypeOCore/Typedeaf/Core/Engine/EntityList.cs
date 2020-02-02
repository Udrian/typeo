using System.Collections.Generic;
using System.Linq;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public class EntityList : IHasTypeO, IHasGame
        {
            TypeO IHasTypeO.TypeO { get; set; }
            private TypeO TypeO { get => (this as IHasTypeO).TypeO; set => (this as IHasTypeO).TypeO = value; }
            public Game Game { get; set; }

            protected List<Entity> Entities;
            protected List<IIsUpdatable> UpdatableEntities;
            protected List<IHasDrawable> HasDrawableEntities;
            protected List<IIsDrawable> IsDrawableEntities;

            public EntityList()
            {
                Entities = new List<Entity>();
                UpdatableEntities = new List<IIsUpdatable>();
                HasDrawableEntities = new List<IHasDrawable>();
                IsDrawableEntities = new List<IIsDrawable>();
            }

            public E Create<E>(Vec2 position = null, Vec2 scale = null, double rotation = 0, Vec2 origin = null, Color color = null, Flipped flipped = Flipped.None) where E : Entity2d, new()
            {
                var entity = CreateEntity<E>() as Entity2d;

                entity.Position = position ?? Vec2.Zero;
                entity.Scale    = scale    ?? Vec2.One;
                entity.Rotation = rotation;
                entity.Origin   = origin   ?? Vec2.Zero;
                entity.Color    = color    ?? Color.White;
                entity.Flipped = flipped;

                return entity as E;
            }

            public E Create<E>() where E : Entity, new()
            {
                return CreateEntity<E>();
            }

            private E CreateEntity<E>() where E : Entity, new()
            {
                var entity = new E();

                (entity as IHasGame)?.SetGame(Game);

                (TypeO as TypeO)?.SetServices(entity);

                entity.Initialize();
                (entity as IHasData)?.EntityData?.Initialize();
                Entities.Add(entity);

                if (entity is IIsUpdatable)
                {
                    UpdatableEntities.Add(entity as IIsUpdatable);
                }

                if (entity is IHasDrawable)
                {
                    HasDrawableEntities.Add(entity as IHasDrawable);
                }

                if (entity is IIsDrawable)
                {
                    IsDrawableEntities.Add(entity as IIsDrawable);
                }

                return entity;
            }

            public void Update(double dt)
            {
                foreach(var entity in UpdatableEntities)
                {
                    if ((entity as Entity)?.WillBeDeleted == true) continue;
                    if (!entity.Pause)
                    {
                        entity.Update(dt);
                    }
                }

                for (int i = Entities.Count - 1; i >= 0; i--)
                {
                    if (Entities[i].WillBeDeleted)
                    {
                        for(int j = 0; j < UpdatableEntities.Count; j++)
                        {
                            if(UpdatableEntities[j] == Entities[i])
                            {
                                UpdatableEntities.RemoveAt(j);
                                break;
                            }
                        }

                        for (int j = 0; j < HasDrawableEntities.Count; j++)
                        {
                            if (HasDrawableEntities[j] == Entities[i])
                            {
                                HasDrawableEntities.RemoveAt(j);
                                break;
                            }
                        }

                        for (int j = 0; j < IsDrawableEntities.Count; j++)
                        {
                            if (IsDrawableEntities[j] == Entities[i])
                            {
                                IsDrawableEntities.RemoveAt(j);
                                break;
                            }
                        }

                        Entities.RemoveAt(i);
                    }
                }
            }

            public void Draw(Canvas canvas)
            {
                foreach (var entity in HasDrawableEntities)
                {
                    if (!entity.Hidden)
                    {
                        entity.DrawDrawable(entity as Entity, canvas);
                    }
                }

                foreach (var entity in IsDrawableEntities)
                {
                    if (!entity.Hidden)
                    {
                        entity.Draw(canvas);
                    }
                }
            }

            public IEnumerable<E> List<E>() where E : Entity
            {
                //TODO: but to dictonary with type 

                return Entities.Where(e => e is E).Cast<E>();
            }
        }
    }
}