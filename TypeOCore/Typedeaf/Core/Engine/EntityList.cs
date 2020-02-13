using System.Collections.Generic;
using System.Linq;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public class EntityList : IHasContext, IHasGame, IHasScene
        {
            Context IHasContext.Context { get; set; }
            private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }
            public Game Game { get; set; }
            public Scene Scene { get; set; }
            public Entity Entity { get; set; }

            protected List<Entity> Entities;
            protected List<IIsUpdatable> Updatables;
            protected List<IHasLogic> HasLogics;
            protected List<IHasDrawable> HasDrawables;
            protected List<IIsDrawable> IsDrawables;
            protected List<IHasEntities> HasEntities;

            public EntityList()
            {
                Entities = new List<Entity>();
                Updatables = new List<IIsUpdatable>();
                HasLogics = new List<IHasLogic>();
                HasDrawables = new List<IHasDrawable>();
                IsDrawables = new List<IIsDrawable>();
                HasEntities = new List<IHasEntities>();
            }

            public E Create<E>(Vec2 position = null, Vec2 scale = null, double rotation = 0, Vec2 origin = null) where E : Entity2d, new()
            {
                var entity = CreateEntity<E>() as Entity2d;

                entity.Position = position ?? entity.Position;
                entity.Scale    = scale    ?? entity.Scale;
                entity.Rotation = rotation;
                entity.Origin   = origin   ?? entity.Origin;

                return entity as E;
            }

            public E Create<E>() where E : Entity, new()
            {
                return CreateEntity<E>();
            }

            private E CreateEntity<E>() where E : Entity, new()
            {
                var entity = new E
                {
                    Parent = Entity
                };

                (entity as IHasData)?.CreateData();

                if(entity is IHasContext)
                {
                    (entity as IHasContext).Context = Context;
                }
                if(entity is IHasGame)
                {
                    (entity as IHasGame).Game = Game;
                }
                if(entity is IHasScene)
                {
                    (entity as IHasScene).Scene = Scene;
                }

                Context.SetServices(entity);

                if (entity is IIsUpdatable)
                {
                    Updatables.Add(entity as IIsUpdatable);
                }

                if (entity is IHasDrawable)
                {
                    var hasDrawableEntity = entity as IHasDrawable;

                    hasDrawableEntity.CreateDrawable(entity);

                    if (hasDrawableEntity.Drawable is IHasGame)
                    {
                        (hasDrawableEntity.Drawable as IHasGame).Game = Game;
                    }

                    hasDrawableEntity.Drawable.Initialize();
                    HasDrawables.Add(hasDrawableEntity);
                }

                if (entity is IIsDrawable)
                {
                    IsDrawables.Add(entity as IIsDrawable);
                }

                if(entity is IHasEntities)
                {
                    var hasEntitiesEntity = entity as IHasEntities;

                    hasEntitiesEntity.Entities = new EntityList()
                    {
                        Game = Game,
                        Scene = Scene,
                        Entity = entity
                    };
                    (hasEntitiesEntity.Entities as IHasContext).Context = Context;

                    HasEntities.Add(hasEntitiesEntity);
                }

                if (entity is IHasLogic)
                {
                    var hasLogicEntity = entity as IHasLogic;
                    hasLogicEntity.CreateLogic(entity);
                    Context.SetServices(hasLogicEntity.Logic);

                    if (hasLogicEntity.Logic is IHasGame)
                    {
                        (hasLogicEntity.Logic as IHasGame).Game = (entity as IHasGame)?.Game;
                    }
                    if (hasLogicEntity.Logic is IHasScene)
                    {
                        (hasLogicEntity.Logic as IHasScene).Scene = Scene;
                    }

                    hasLogicEntity.Logic.Initialize();
                    HasLogics.Add(hasLogicEntity);
                }

                entity.Initialize();
                Entities.Add(entity);

                return entity;
            }

            public void Update(double dt)
            {
                foreach(var entity in Updatables)
                {
                    if ((entity as Entity)?.WillBeDeleted == true) continue;
                    if (!entity.Pause)
                    {
                        entity.Update(dt);
                    }
                }

                foreach(var entity in HasLogics)
                {
                    if ((entity as Entity)?.WillBeDeleted == true) continue;
                    if (!entity.PauseLogic)
                    {
                        entity.Logic.Update(dt);
                    }
                }

                foreach(var entity in HasEntities)
                {
                    if ((entity as Entity)?.WillBeDeleted == true) continue;
                    if ((entity as IIsUpdatable)?.Pause == true) continue;
                    entity.Entities.Update(dt);
                }

                //Remove entities
                for (int i = Entities.Count - 1; i >= 0; i--)
                {
                    if (Entities[i].WillBeDeleted)
                    {
                        for(int j = 0; j < Updatables.Count; j++)
                        {
                            if(Updatables[j] == Entities[i])
                            {
                                Updatables.RemoveAt(j);
                                break;
                            }
                        }

                        for (int j = 0; j < HasDrawables.Count; j++)
                        {
                            if (HasDrawables[j] == Entities[i])
                            {
                                HasDrawables.RemoveAt(j);
                                break;
                            }
                        }

                        for (int j = 0; j < IsDrawables.Count; j++)
                        {
                            if (IsDrawables[j] == Entities[i])
                            {
                                IsDrawables.RemoveAt(j);
                                break;
                            }
                        }

                        for (int j = 0; j < HasEntities.Count; j++)
                        {
                            if (HasEntities[j] == Entities[i])
                            {
                                HasEntities.RemoveAt(j);
                                break;
                            }
                        }

                        Entities.RemoveAt(i);
                    }
                }
            }

            public void Draw(Canvas canvas)
            {
                foreach (var entity in HasDrawables)
                {
                    if (!entity.Hidden)
                    {
                        entity.Drawable.Draw(canvas);
                    }
                }

                foreach (var entity in IsDrawables)
                {
                    if (!entity.Hidden)
                    {
                        entity.Draw(canvas);
                    }
                }

                foreach (var entity in HasEntities)
                {
                    if ((entity as IIsDrawable)?.Hidden == true) continue;
                    if ((entity as IHasDrawable)?.Hidden == true) continue;
                    entity.Entities.Draw(canvas);
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