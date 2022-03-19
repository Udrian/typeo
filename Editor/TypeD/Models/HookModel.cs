using System;
using System.Collections.Generic;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class HookModel : IHookModel, IModel
    {
        private Dictionary<string, List<Action<object>>> Hooks { get; set; }

        // Constructors
        public HookModel()
        {
            ClearHooks();
        }

        public void Init(IResourceModel resourceModel)
        {
        }

        // Functions
        public void ClearHooks()
        {
            Hooks = new Dictionary<string, List<Action<object>>>();
        }

        public void AddHook(string hook, Action<object> action)
        {
            if (!Hooks.ContainsKey(hook)) Hooks.Add(hook, new List<Action<object>>());
            Hooks[hook].Add(action);
        }

        public void AddHook<T>(Action<T> action) where T : Hook, new()
        {
            AddHook(Activator.CreateInstance(typeof(T)).ToString(), (a) => { action(a as T); });
        }

        public void RemoveHook(string hook)
        {
            Hooks.Remove(hook);
        }

        public void RemoveHook<T>() where T : Hook, new()
        {
            RemoveHook(Activator.CreateInstance(typeof(T)).ToString());
        }

        public void Shoot(string hook, object param)
        {
            if (!Hooks.ContainsKey(hook)) return;
            foreach(var h in Hooks[hook])
            {
                h(param);
            }
        }

        public void Shoot<T>(T hook) where T : Hook, new()
        {
            Shoot(hook.ToString(), hook);
        }
    }
}
