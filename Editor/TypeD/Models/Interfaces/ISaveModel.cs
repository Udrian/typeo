using System.Threading.Tasks;

namespace TypeD.Models.Interfaces
{
    public interface ISaveModel : IModel
    {
        public abstract class SaveContext
        {
            public abstract void Init(IResourceModel resourceModel, object param = null);
            public abstract Task SaveAction();
            internal bool ShouldSave { get; set; }
        }

        public bool AnythingToSave { get; }
        public void AddSave<T>(object param = null) where T : SaveContext, new();
        public bool SaveContextExists<T>() where T : SaveContext, new();
        public T GetSaveContext<T>(object param = null) where T : SaveContext, new();
        public Task Save();
    }
}
