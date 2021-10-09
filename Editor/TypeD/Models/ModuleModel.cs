using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class ModuleModel : IModuleModel
    {
        // Paths
        public static string ModuleCachePath { get { return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/TypeO/ModulesCache"; } }

        // Constructors
        public ModuleModel()
        {
        }

        // Functions
        public async Task<bool> Download(Module module)
        {
            if (Directory.Exists($"{module.ModulePath}")) return false;

            Directory.CreateDirectory(module.ModulePath);
            var zipName = $"{module.Name}-{module.Version}.zip";
            var moduleUrl = new Uri($"https://typedeaf.nyc3.cdn.digitaloceanspaces.com/typeo/releases/modules/{module.Name}/{zipName}");
            var downloadZipPath = $"{module.ModulePath}/{zipName}";

            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(moduleUrl, downloadZipPath);
            }
            await Task.Run(() =>
            {
                ZipFile.ExtractToDirectory(downloadZipPath, module.ModulePath);
            });

            await Task.Run(() =>
            {
                File.Delete(downloadZipPath);
            });

            LoadAssembly(module);
            return true;
            //TODO progress bar
        }

        // Internal functions
        internal void LoadAssembly(Module module)
        {
            if (!File.Exists(module.ModuleDLLPath)) return;
            module.Assembly = System.Reflection.Assembly.LoadFrom(module.ModuleDLLPath);

            module.ModuleTypeInfo = GetModuleType(module);
        }

        internal void AddToProjectXML(Module module, XElement project)
        {
            AddReference(module, GetItemGroup(project, "Debug"));
            AddReference(module, GetItemGroup(project, "Release"));
        }

        private System.Reflection.TypeInfo GetModuleType(Module module)
        {
            if (module.Name == "TypeOCore") return null;
            System.Reflection.TypeInfo moduleType = null;
            foreach (var type in module.Assembly.DefinedTypes)
            {
                if (type.IsSubclassOf(typeof(TypeOEngine.Typedeaf.Core.Engine.Module)))
                {
                    moduleType = type;
                    break;
                }
            }

            return moduleType;
        }

        private static XElement GetItemGroup(XElement project, string configuration)
        {
            var configString = $"'$(Configuration)' == '{configuration}'";
            var itemGroups = project.Elements("ItemGroup");
            XElement itemGroup = null;

            foreach (var item in itemGroups)
            {
                if (item.Attribute("Condition")?.Value == configString)
                {
                    itemGroup = item;
                }
            }
            if (itemGroup == null)
            {
                itemGroup = new XElement("ItemGroup");
                itemGroup.Add(new XAttribute("Condition", configString));
                project.Add(itemGroup);
            }
            return itemGroup;
        }

        private void AddReference(Module module, XElement itemGroup)
        {
            var foundModule = false;
            foreach (var referenceElement in itemGroup.Elements("Reference"))
            {
                if (referenceElement.Attribute("Include")?.Value == module.Name)
                {
                    foundModule = true;
                    break;
                }
            }
            if (!foundModule)
            {
                var referenceElement = new XElement("Reference");
                referenceElement.Add(new XAttribute("Include", module.Name));

                referenceElement.Add(new XElement("HintPath", module.ModuleDLLPath));

                itemGroup.Add(referenceElement);
            }
        }
    }
}
