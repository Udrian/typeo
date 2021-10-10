namespace TypeD.Models.Interfaces
{
    public interface IResourceModel
    {
        public void Add(string key, object value);
        public T Get<T>(string key) where T : class;
    }
}
