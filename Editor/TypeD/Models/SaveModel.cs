using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class SaveModel : ISaveModel
    {
        // Data
        public Dictionary<string, Func<Task>> SaveActions { get; set; }

        // Constructors
        public SaveModel()
        {
            SaveActions = new Dictionary<string, Func<Task>>();
        }

        // Functions
        public bool AnythingToSave { get { return SaveActions.Count != 0; } }

        public void AddSave(string context, Func<Task> action)
        {
            if (!SaveActions.ContainsKey(context))
            {
                SaveActions.Add(context, action);
            }
        }

        public async Task Save()
        {
            foreach(var saveAction in SaveActions.Values)
            {
                await saveAction();
            }
            SaveActions.Clear();
        }
    }
}
