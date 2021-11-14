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
    public class TypeOTypeProvider : ITypeOTypeProvider, IProvider
    {
        private static string TypeOTypeFileEnding = "type";

        // Model
        IResourceModel ResourceModel { get; set; }
        IProjectModel ProjectModel { get; set; }
        ISaveModel SaveModel { get; set; }

        // Constructors
        public TypeOTypeProvider()
        {
        }

        public void Init(IResourceModel resourceModel)
        {
            ResourceModel = resourceModel;

            ProjectModel = ResourceModel.Get<IProjectModel>();
            SaveModel = ResourceModel.Get<ISaveModel>();
        }

        // Functions
        public TypeOType Load(Project project, string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return null;
            var path = GetPath(project, fullName);

            return LoadFromPath(path);
        }


        private TypeOType LoadFromPath(string path)
        {
            if (!File.Exists(path)) return null;
            return JSON.Deserialize<TypeOType>(path);
        }

        public void Save(Project project, TypeOType typeOType)
        {
            var typeOTypes = SaveModel.GetSaveContext<List<TypeOType>>("TypeOTypes") ?? new List<TypeOType>();
            typeOTypes.Add(typeOType);

            SaveModel.AddSave("TypeOTypes", typeOTypes, (context) =>
            {
                return Task.Run(() => {
                    var saveTypeOTypes = context as List<TypeOType>;
                    foreach(var saveTypeOType in saveTypeOTypes)
                    {
                        JSON.Serialize(saveTypeOType, GetPath(project, saveTypeOType.FullName));
                    }
                    ProjectModel.BuildTypeOTypeTree(project);
                });
            });
        }

        public bool Exists(Project project, TypeOType typeOType)
        {
            return File.Exists(GetPath(project, typeOType.FullName));
        }

        public List<TypeOType> ListAll(Project project)
        {
            var path = GetPath(project);
            if (!Directory.Exists(path)) return new List<TypeOType>();

            var files = Directory.GetFiles(path, $"*.{TypeOTypeFileEnding}", SearchOption.AllDirectories);
            var typeOTypes = files.Select((f) => { return LoadFromPath(f); }).ToList();

            var unsavedTypeOTypes = SaveModel.GetSaveContext<List<TypeOType>>("TypeOTypes") ?? new List<TypeOType>();

            return typeOTypes.Union(unsavedTypeOTypes)
                             .GroupBy(t => t.FullName)
                             .Select(t => t.First())
                             .ToList();
        }

        // Internal
        private string GetPath(Project project, string fullName)
        {
            return Path.Combine(GetPath(project), $"{fullName.Replace(".", @"\")}.{TypeOTypeFileEnding}");
        }

        private string GetPath(Project project)
        {
            return Path.Combine(project.ProjectTypeOPath, "types");
        }
    }
}
