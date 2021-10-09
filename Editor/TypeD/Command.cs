using System;
using System.Collections.Generic;
using TypeD.Commands.Module;

namespace TypeD
{
    public class Command
    {
        public static ModuleCommand Module  { get { return Get<ModuleCommand>();  } }

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
