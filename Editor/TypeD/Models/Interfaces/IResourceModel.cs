namespace TypeD.Models.Interfaces
{
    public interface IResourceModel : IModel
    {
        public void Add(string key, object value);
        public void Remove(string key);
        public T Get<T>(string key) where T : class;
        public T Get<T>() where T : class;
    }
}
