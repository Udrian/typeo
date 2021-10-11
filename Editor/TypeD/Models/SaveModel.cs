using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class SaveModel : ISaveModel
    {
        // Data
        public List<Func<Task>> SaveActions { get; set; }

        // Constructors
        public SaveModel()
        {
            SaveActions = new List<Func<Task>>();
        }

        // Functions
        public bool AnythingToSave { get; set; }

        public void AddSave(Func<Task> action)
        {
            AnythingToSave = true;
            SaveActions.Add(action);
        }

        public async Task Save()
        {
            AnythingToSave = false;
            foreach(var saveAction in SaveActions)
            {
                await saveAction();
            }
            SaveActions.Clear();
        }
    }
}
