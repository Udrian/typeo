using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class SaveModel : ISaveModel
    {
        private class SaveContext
        {
            public Func<object, Task> SaveAction { get; set; }
            public object Context { get; set; }
        }

        // Data
        private Dictionary<string, SaveContext> SaveContexts { get; set; }

        // Models
        IResourceModel ResourceModel { get; set; }
        IUINotifyModel UINotifyModel { get; set; }

        // Constructors
        public SaveModel()
        {
            SaveContexts = new Dictionary<string, SaveContext>();
        }

        public void Init(IResourceModel resourceModel)
        {
            ResourceModel = resourceModel;

            UINotifyModel = ResourceModel.Get<IUINotifyModel>();
        }

        // Functions
        public bool AnythingToSave { get { return SaveContexts.Count != 0; } }

        public void AddSave(string contextId, Func<Task> saveAction)
        {
            AddSave(contextId, null, (o) => { return saveAction(); });
        }

        public void AddSave(string contextId, object context, Func<object, Task> saveAction)
        {
            if(!SaveContextExists(contextId))
            {
                SaveContexts.Add(contextId, new SaveContext() { Context = context, SaveAction = saveAction });

                UINotifyModel.Notify("AnythingToSave");
            }
        }

        public bool SaveContextExists(string contextId)
        {
            return SaveContexts.ContainsKey(contextId);
        }

        public T GetSaveContext<T>(string contextId)
        {
            if (!SaveContextExists(contextId)) return default;
            return (T)SaveContexts[contextId].Context;
        }

        public async Task Save()
        {
            foreach(var saveContext in SaveContexts.Values)
            {
                await saveContext.SaveAction(saveContext.Context);
            }
            SaveContexts.Clear();
            UINotifyModel.Notify("AnythingToSave");
        }
    }
}
