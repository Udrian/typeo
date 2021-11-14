using System.Windows;
using TypeD.Models.Interfaces;

namespace TypeDitor.Models
{
    public class ResourceModel : IResourceModel, IModel
    {
        ResourceDictionary Resources { get; set; }

        // Constructors
        public ResourceModel(ResourceDictionary resources)
        {
            Resources = resources;
        }

        public void Init(IResourceModel resourceModel)
        {
            // This is not used
        }

        // Functions
        public void Add(string key, object value)
        {
            var providerResource = new ResourceDictionary
            {
                { key, value }
            };
            Resources.MergedDictionaries.Add(providerResource);
        }

        public T Get<T>(string key) where T : class
        {
            if(!Resources.Contains(key))
                return null;
            return Resources[key] as T;
        }

        public T Get<T>() where T : class
        {
            var type = typeof(T);
            if(type.IsInterface && type.Name.StartsWith("I"))
            {
                return Get<T>(type.Name.Substring(1));
            }
            return Get<T>(type.Name);
        }
    }
}
