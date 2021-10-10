using System.Windows;
using TypeD.Models.Interfaces;

namespace TypeDitor.Models
{
    public class ResourceModel : IResourceModel
    {
        internal ResourceDictionary Resources { get; set; }

        // Constructors
        public ResourceModel()
        {
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
    }
}
