using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;
using TypeD.Models.Providers.Interfaces;

namespace TypeD.Models.Providers
{
    public class ComponentProvider : IComponentProvider, IProvider
    {
        private static string ComponentFileEnding = "component";

        // Model
        IResourceModel ResourceModel { get; set; }
        IProjectModel ProjectModel { get; set; }
        ISaveModel SaveModel { get; set; }

        // Constructors
        public ComponentProvider()
        {
        }

        public void Init(IResourceModel resourceModel)
        {
            ResourceModel = resourceModel;

            ProjectModel = ResourceModel.Get<IProjectModel>();
            SaveModel = ResourceModel.Get<ISaveModel>();
        }

        // Functions
        public Component Load(Project project, string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return null;
            var path = GetPath(project, fullName);

            return LoadFromPath(path);
        }


        private Component LoadFromPath(string path)
        {
            if (!File.Exists(path)) return null;
            return JSON.Deserialize<Component>(path);
        }

        public void Save(Project project, Component component)
        {
            var components = SaveModel.GetSaveContext<List<Component>>("Components") ?? new List<Component>();
            components.Add(component);

            SaveModel.AddSave("Components", components, (context) =>
            {
                return Task.Run(() => {
                    var saveComponents = context as List<Component>;
                    foreach(var saveComponent in saveComponents)
                    {
                        JSON.Serialize(saveComponent, GetPath(project, saveComponent.FullName));
                    }
                    ProjectModel.BuildComponentTree(project);
                });
            });
        }

        public bool Exists(Project project, Component component)
        {
            return File.Exists(GetPath(project, component.FullName));
        }

        public List<Component> ListAll(Project project)
        {
            var path = GetPath(project);
            if (!Directory.Exists(path)) return new List<Component>();

            var files = Directory.GetFiles(path, $"*.{ComponentFileEnding}", SearchOption.AllDirectories);
            var components = files.Select((f) => { return LoadFromPath(f); }).ToList();

            var unsavedComponents = SaveModel.GetSaveContext<List<Component>>("Components") ?? new List<Component>();

            return components.Union(unsavedComponents)
                             .GroupBy(t => t.FullName)
                             .Select(t => t.First())
                             .ToList();
        }

        // Internal
        private string GetPath(Project project, string fullName)
        {
            return Path.Combine(GetPath(project), $"{fullName.Replace(".", @"\")}.{ComponentFileEnding}");
        }

        private string GetPath(Project project)
        {
            return Path.Combine(project.ProjectTypeOPath, "components");
        }
    }
}
