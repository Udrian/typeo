using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace TypeD.Models
{
    public class ModuleModel
    {
        // Data
        public string Name { get; set; }
        public string Version { get; set; }

        // Loads
        private Assembly Assembly { get; set; }
        public TypeInfo ModuleTypeInfo { get; set; }
        
        // Constructors
        public ModuleModel(string name, string version)
        {
            Name = name;
            Version = version;
            Assembly = Assembly.LoadFrom(ModuleDLLPath);

            ModuleTypeInfo = GetModuleType();
        }

        // Paths
        public static string ModuleCachePath { get { return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/TypeO/ModulesCache"; } }
        private string ModulePath { get { return Path.Combine(ModuleCachePath, Name, Version, Name); } }
        private string ModuleDLLPath { get { return Path.Combine(ModulePath, $"{Name}.dll"); } }

        public void AddToProjectXML(XElement project)
        {
            AddReference(GetItemGroup(project, "Debug"));
            AddReference(GetItemGroup(project, "Release"));
        }

        // Private functions
        private TypeInfo GetModuleType()
        {
            if (Name == "TypeOCore") return null;
            TypeInfo moduleType = null;
            foreach (var type in Assembly.DefinedTypes)
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

        private void AddReference(XElement itemGroup)
        {
            var foundModule = false;
            foreach (var referenceElement in itemGroup.Elements("Reference"))
            {
                if (referenceElement.Attribute("Include")?.Value == Name)
                {
                    foundModule = true;
                    break;
                }
            }
            if (!foundModule)
            {
                var referenceElement = new XElement("Reference");
                referenceElement.Add(new XAttribute("Include", Name));

                referenceElement.Add(new XElement("HintPath", ModuleDLLPath));

                itemGroup.Add(referenceElement);
            }
        }
    }
}
