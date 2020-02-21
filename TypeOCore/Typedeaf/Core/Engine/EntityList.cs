using System;
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
        public class EntityList : IHasContext, IHasScene
        {
            Context IHasContext.Context { get; set; }
            private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }
            public ILogger Logger { get; set; }

            public Scene Scene { get; set; }
            public Entity Entity { get; set; }

            protected List<Entity> Entities;
            protected List<IIsUpdatable> Updatables;
            protected List<IHasLogic> HasLogics;
            protected List<IHasDrawable> HasDrawables;
            protected List<IIsDrawable> IsDrawables;
            protected List<IHasEntities> HasEntities;
            protected Dictionary<Type, IEnumerable<Entity>> EntityLists;

            internal EntityList()
            {
                Entities = new List<Entity>();
                Updatables = new List<IIsUpdatable>();
                HasLogics = new List<IHasLogic>();
                HasDrawables = new List<IHasDrawable>();
                IsDrawables = new List<IIsDrawable>();
                HasEntities = new List<IHasEntities>();
                EntityLists = new Dictionary<Type, IEnumerable<Entity>>();
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

                Logger.Log(LogLevel.Debug, $"Creating Entity of type '{typeof(E).FullName}'");
                Context.InitializeObject(entity, this);
                entity.Initialize();

                if (entity is IIsUpdatable)
                {
                    Updatables.Add(entity as IIsUpdatable);
                }

                if (entity is IHasDrawable)
                {
                    HasDrawables.Add(entity as IHasDrawable);
                }

                if (entity is IIsDrawable)
                {
                    IsDrawables.Add(entity as IIsDrawable);
                }

                if(entity is IHasEntities)
                {
                    HasEntities.Add(entity as IHasEntities);
                }

                if (entity is IHasLogic)
                {
                    HasLogics.Add(entity as IHasLogic);
                }

                var eType = typeof(E);
                if (EntityLists.ContainsKey(eType))
                {
                    EntityLists[eType] = Entities.Where(e => e is E).Cast<E>().ToList();
                }

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

                        var iType = Entities[i].GetType();
                        if (EntityLists.ContainsKey(iType))
                        {
                            EntityLists.Remove(iType);
                        }

                        Logger.Log(LogLevel.Debug, $"Removing Entity of type '{Entities[i].GetType().FullName}'");
                        Entities[i].Cleanup();
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

            public List<E> List<E>() where E : Entity
            {
                var eType = typeof(E);
                if (!EntityLists.ContainsKey(eType))
                {
                    EntityLists.Add(eType, Entities.Where(e => e is E).Cast<E>().ToList());
                }

                return EntityLists[eType] as List<E>;
            }
        }
    }
}