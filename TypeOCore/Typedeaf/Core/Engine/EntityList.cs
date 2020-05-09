using System;
using System.Collections.Generic;
using System.Linq;
using TypeOEngine.Typedeaf.Core.Collections;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Engine
    {
        public class EntityList : IHasContext, IHasScene, IHasEntity
        {
            Context IHasContext.Context { get; set; }
            private Context Context { get => (this as IHasContext).Context; set => (this as IHasContext).Context = value; }
            private ILogger Logger { get; set; }

            public Scene Scene { get; set; }
            public Entity Entity { get; set; } //TODO: This maybe should change to something else, OwnerEntity or Node?

            private DelayedList<Entity> Entities { get; set; }

            private DelayedList<IIsUpdatable> Updatables { get; set; }
            private DelayedList<IHasEntities> HasEntities { get; set; }

            private Dictionary<Type, IEnumerable<Entity>> EntityLists { get; set; }
            private Dictionary<string, Entity> EntityIDs { get; set; }

            private Dictionary<Type, Stub> Stubs { get; set; }

            internal EntityList()
            {
                Entities = new DelayedList<Entity>();

                Updatables = new DelayedList<IIsUpdatable>();
                HasEntities = new DelayedList<IHasEntities>();

                EntityLists = new Dictionary<Type, IEnumerable<Entity>>();
                EntityIDs = new Dictionary<string, Entity>();

                Stubs = new Dictionary<Type, Stub>();
            }

            public E Create<E>(Vec2? position = null, Vec2? scale = null, double rotation = 0, Vec2? origin = null, bool pushToDrawStack = true) where E : Entity2d, new() //TODO: Split out
            {
                var entity = Create<E>() as Entity2d;

                entity.Position = position ?? entity.Position;
                entity.Scale = scale ?? entity.Scale;
                entity.Rotation = rotation;
                entity.Origin = origin ?? entity.Origin;

                return entity as E;
            }

            public E Create<E>() where E : Entity, new() //TODO: Split out, Should be able to push automatically to draw stack
            {
                var entity = new E
                {
                    Parent = Entity,
                    ParentEntityList = this,
                    DrawStack = Scene?.DrawStack ?? Entity?.DrawStack //TODO: Change this to be from same interface
                };

                Logger.Log(LogLevel.Debug, $"Creating Entity of type '{typeof(E).FullName}'");
                Context.InitializeObject(entity, this);
                entity.Initialize();

                Entities.Add(entity);
                var eType = typeof(E);
                if(EntityLists.ContainsKey(eType))
                {
                    EntityLists[eType] = Entities.Where(e => e is E).Cast<E>().ToList();
                }

                if(string.IsNullOrEmpty(entity.ID))
                {
                    entity.ID = Guid.NewGuid().ToString();
                }
                EntityIDs.Add(entity.ID, entity);

                if(entity is IIsUpdatable updatable)
                {
                    Updatables.Add(updatable);
                }

                if(entity is IHasEntities hasEntities)
                {
                    HasEntities.Add(hasEntities);
                }

                return entity;
            }

            public Entity CreateFromStub<S>() where S : Stub, new() //TODO: Split out
            {
                var sType = typeof(S);
                if(!Stubs.ContainsKey(sType))
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

            internal void AddUpdatable(IIsUpdatable updatable) //TODO: Look over this
            {
                Updatables.Add(updatable);
            }

            internal void RemoveUpdatable(IIsUpdatable updatable) //TODO: Look over this
            {
                Updatables.Remove(updatable);
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

                foreach(var entity in HasEntities)
                {
                    if((entity as Entity)?.WillBeDeleted == true) continue;
                    if((entity as IIsUpdatable)?.Pause == true) continue;
                    entity.Entities.Update(dt);
                }

                //Remove entities
                for(int i = Entities.Count - 1; i >= 0; i--) //TODO: Look over this, change to queue instead
                {
                    if(Entities[i].WillBeDeleted)
                    {
                        for(int j = 0; j < Updatables.Count; j++)
                        {
                            if(Updatables[j] is Logic logic) //TODO: Do I want this? No, I don't
                            {
                                if(logic.Parent == Entities[i])
                                {
                                    Updatables.RemoveAt(j);
                                }
                            }
                            else if(Updatables[j] == Entities[i])
                            {
                                Updatables.RemoveAt(j);
                                break;
                            }
                        }

                        for(int j = 0; j < HasEntities.Count; j++)
                        {
                            if(HasEntities[j] == Entities[i])
                            {
                                HasEntities.RemoveAt(j);
                                break;
                            }
                        }

                        var iType = Entities[i].GetType();
                        if(EntityLists.ContainsKey(iType))
                        {
                            EntityLists.Remove(iType);
                        }

                        Logger.Log(LogLevel.Debug, $"Removing Entity of type '{Entities[i].GetType().FullName}'");
                        Entities[i].Cleanup();
                        Entities.RemoveAt(i);
                    }
                }

                Entities.Process();
                Updatables.Process();
                HasEntities.Process();
            }

            public List<E> List<E>() where E : Entity
            {
                var eType = typeof(E);
                if(!EntityLists.ContainsKey(eType))
                {
                    EntityLists.Add(eType, Entities.Where(e => e is E).Cast<E>().ToList());
                }

                return EntityLists[eType] as List<E>;
            }

            public List<Entity> ListAll()
            {
                return new List<Entity>(Entities);
            }

            public E GetEntityByID<E>(string id) where E : Entity
            {
                if(!EntityIDs.ContainsKey(id))
                    return null;
                var entity = EntityIDs[id] as E;
                if(entity == null)
                    Logger.Log(LogLevel.Warning, $"Entity with id '{id}' is not of type '{typeof(E).FullName}'");
                return entity;
            }
        }
    }
}