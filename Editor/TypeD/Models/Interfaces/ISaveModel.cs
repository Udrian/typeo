using System;
using System.Threading.Tasks;

namespace TypeD.Models.Interfaces
{
    public interface ISaveModel
    {
        public bool AnythingToSave { get; }
        public void AddSave(string contextId, Func<Task> saveAction);
        public void AddSave(string contextId, object context, Func<object, Task> saveAction);
        public bool SaveContextExists(string contextId);
        public T GetSaveContext<T>(string contextId);
        public Task Save();
    }
}
