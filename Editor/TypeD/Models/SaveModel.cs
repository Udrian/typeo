using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class SaveModel : ISaveModel
    {
        // Data
        private Dictionary<Type, ISaveModel.SaveContext> SaveContexts { get; set; }

        // Models
        IResourceModel ResourceModel { get; set; }
        IUINotifyModel UINotifyModel { get; set; }

        // Constructors
        public SaveModel()
        {
            SaveContexts = new Dictionary<Type, ISaveModel.SaveContext>();
        }

        public void Init(IResourceModel resourceModel)
        {
            ResourceModel = resourceModel;

            UINotifyModel = ResourceModel.Get<IUINotifyModel>();
        }

        // Properties
        public bool AnythingToSave { get { return SaveContexts.Any(c => c.Value.ShouldSave); } }

        // Functions
        public void AddSave<T>(object param = null) where T : ISaveModel.SaveContext, new()
        {
            var context = GetSaveContext<T>(param);
            if(!context.ShouldSave)
            {
                context.ShouldSave = true;

                UINotifyModel.Notify("AnythingToSave");
            }
        }

        public bool SaveContextExists<T>() where T : ISaveModel.SaveContext, new()
        {
            return SaveContexts.ContainsKey(typeof(T));
        }

        public T GetSaveContext<T>(object param = null) where T : ISaveModel.SaveContext, new()
        {
            if (!SaveContextExists<T>())
            {
                var newContext = new T();
                SaveContexts.Add(typeof(T), newContext);
                newContext.Init(ResourceModel, param);
            }
            return SaveContexts[typeof(T)] as T;
        }

        public async Task Save()
        {
            foreach(var saveContext in SaveContexts.Values)
            {
                if(saveContext.ShouldSave)
                    await saveContext.SaveAction();
            }
            SaveContexts.Clear();
            UINotifyModel.Notify("AnythingToSave");
        }
    }
}
