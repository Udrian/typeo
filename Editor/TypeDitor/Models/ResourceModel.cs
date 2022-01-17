using System.Windows;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

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
            // If this is a Model or Provider then Init
            if(value is IModel)
            {
                (value as IModel).Init(this);
            }
            else if(value is IProvider)
            {
                (value as IProvider).Init(this);
            }

            var resource = new ResourceDictionary
            {
                { key, value }
            };
            Resources.MergedDictionaries.Add(resource);
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
