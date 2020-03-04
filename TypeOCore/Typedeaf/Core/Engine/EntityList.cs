﻿using System;
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

            private List<Entity> Entities { get; set; }
            private List<Entity> EntitiesToAdd { get; set; }

            private List<Entity> Updatables { get; set; }
            private List<Entity> Drawables { get; set; }
            private List<IHasEntities> HasEntities { get; set; }

            private Dictionary<Type, IEnumerable<Entity>> EntityLists { get; set; }
            private Dictionary<string, Entity> EntityIDs { get; set; }

            private Dictionary<Type, Stub> Stubs { get; set; }

            internal EntityList()
            {
                Entities = new List<Entity>();
                EntitiesToAdd = new List<Entity>();

                Updatables = new List<Entity>();
                Drawables = new List<Entity>();
                HasEntities = new List<IHasEntities>();

                EntityLists = new Dictionary<Type, IEnumerable<Entity>>();
                EntityIDs = new Dictionary<string, Entity>();

                Stubs = new Dictionary<Type, Stub>();
            }

            public E Create<E>(Vec2 position = null, Vec2 scale = null, double rotation = 0, Vec2 origin = null) where E : Entity2d, new()
            {
                var entity = Create<E>() as Entity2d;

                entity.Position = position ?? entity.Position;
                entity.Scale    = scale    ?? entity.Scale;
                entity.Rotation = rotation;
                entity.Origin   = origin   ?? entity.Origin;

                return entity as E;
            }

            public E Create<E>() where E : Entity, new()
            {
                var entity = new E
                {
                    Parent = Entity
                };

                Logger.Log(LogLevel.Debug, $"Creating Entity of type '{typeof(E).FullName}'");
                Context.InitializeObject(entity, this);
                entity.Initialize();

                var eType = typeof(E);
                if (EntityLists.ContainsKey(eType))
                {
                    EntityLists[eType] = Entities.Where(e => e is E).Cast<E>().ToList();
                }

                if (string.IsNullOrEmpty(entity.ID))
                {
                    entity.ID = Guid.NewGuid().ToString();
                }
                EntityIDs.Add(entity.ID, entity);
                EntitiesToAdd.Add(entity);

                return entity;
            }

            public Entity CreateFromStub<S>() where S : Stub, new()
            {
                var sType = typeof(S);
                if (!Stubs.ContainsKey(sType))
                {
                    var nStub = new S();
                    Logger.Log(LogLevel.Debug, $"Creating Stub of type '{typeof(S).FullName}'");
                    Context.InitializeObject(nStub, this);
                    nStub.Initialize();
                    Stubs.Add(sType, nStub);
                }

                var stub = Stubs[sType];
                Logger.Log(LogLevel.Debug, $"Creating Entity from Stub '{typeof(S).FullName}'");
                var entity = stub.CreateEntity(this);
                return entity;
            }

            public E CreateFromStub<S, E>() where S : Stub<E>, new() where E : Entity, new()
            {
                var entity = CreateFromStub<S>() as E;
                if(entity == null)
                {
                    Logger.Log(LogLevel.Warning, $"Could not create entity '{typeof(E).FullName}' from Stub '{typeof(S).FullName}'");
                }
                return entity;
            }

            private void AddEntity(Entity entity)
            {
                Logger.Log(LogLevel.Debug, $"Entity of type '{entity.GetType().FullName}' added");

                if (entity is IIsUpdatable || entity is IHasLogic)
                {
                    Updatables.Add(entity);
                }

                if (entity is IHasDrawable || entity is IIsDrawable)
                {
                    Drawables.Add(entity);
                }

                if (entity is IHasEntities hasEntities)
                {
                    HasEntities.Add(hasEntities);
                }

                Entities.Add(entity);
            }

            public void Update(double dt)
            {
                foreach(var entity in Updatables)
                {
                    if (entity.WillBeDeleted) continue;

                    if (entity is IIsUpdatable isUpdatable)
                    {
                        if (!isUpdatable.Pause)
                        {
                            isUpdatable.Update(dt);
                        }
                    }
                    if (entity is IHasLogic hasLogic)
                    {
                        if (!hasLogic.PauseLogic)
                        {
                            hasLogic.Logic.Update(dt);
                        }
                    }
                }

                foreach(var entity in HasEntities)
                {
                    if ((entity as Entity)?.WillBeDeleted == true) continue;
                    if ((entity as IIsUpdatable)?.Pause == true) continue;
                    entity.Entities.Update(dt);
                }

                //Add entities
                foreach(var entity in EntitiesToAdd)
                {
                    AddEntity(entity);
                }
                EntitiesToAdd.Clear();

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

                        for (int j = 0; j < Drawables.Count; j++)
                        {
                            if (Drawables[j] == Entities[i])
                            {
                                Drawables.RemoveAt(j);
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
                foreach (var entity in Drawables)
                {
                    if (entity.WillBeDeleted) continue;

                    if (entity is IHasDrawable hasDrawable)
                    {
                        if (!hasDrawable.Hidden)
                        {
                            hasDrawable.Drawable.Draw(canvas);
                        }
                    }
                    if (entity is IIsDrawable isDrawable)
                    {
                        if (!isDrawable.Hidden)
                        {
                            isDrawable.Draw(canvas);
                        }
                    }
                }

                foreach (var entity in HasEntities)
                {
                    if ((entity as Entity)?.WillBeDeleted == true) continue;
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

            public E GetEntityByID<E>(string id) where E : Entity
            {
                if (!EntityIDs.ContainsKey(id))
                    return null;
                var entity = EntityIDs[id] as E;
                if (entity == null)
                    Logger.Log(LogLevel.Warning, $"Entity with id '{id}' is not of type '{typeof(E).FullName}'");
                return entity;
            }
        }
    }
}