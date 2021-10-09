using System;
using System.Collections.Generic;
using TypeD.Commands.Entity;
using TypeD.Commands.Game;
using TypeD.Commands.Module;
using TypeD.Commands.Scene;

namespace TypeD
{
    public class Command
    {
        public static EntityCommand  Entity  { get { return Get<EntityCommand>();  } }
        public static GameCommand Game    { get { return Get<GameCommand>();    } }
        public static ModuleCommand Module  { get { return Get<ModuleCommand>();  } }
        public static SceneCommand Scene   { get { return Get<SceneCommand>();   } }

        private static Dictionary<Type, Command> Commands { get; set; } = new Dictionary<Type, Command>();
        public static void Add<T>() where T : Command, new()
        {
            if(!Initialized<T>())
                Commands.Add(typeof(T), new T());
        }
        public static T Get<T>() where T : Command, new()
        {
            if(!Initialized<T>())
            {
                Add<T>();
            }
            return Commands[typeof(T)] as T;
        }
        public static bool Initialized<T>() where T: Command, new()
        {
            return Commands.ContainsKey(typeof(T));
        }
    }
}
