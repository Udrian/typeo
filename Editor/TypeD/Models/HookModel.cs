using System;
using System.Collections.Generic;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class HookModel : IHookModel, IModelProvider
    {
        private Dictionary<string, List<Action<object>>> Hooks { get; set; }

        // Constructions
        public HookModel()
        {
            ClearHooks();
        }

        public void Init(IResourceModel resourceModel)
        {
        }

        public void ClearHooks()
        {
            Hooks = new Dictionary<string, List<Action<object>>>();
        }

        // Functions
        public void AddHook(string hook, Action<object> action)
        {
            if (!Hooks.ContainsKey(hook)) Hooks.Add(hook, new List<Action<object>>());
            Hooks[hook].Add(action);
        }

        public void Shoot(string hook, object param)
        {
            if (!Hooks.ContainsKey(hook)) return;
            foreach(var h in Hooks[hook])
            {
                h(param);
            }
        }
    }
}
