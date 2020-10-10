using System.IO;
using System.Xml.Linq;

namespace TypeD.Model
{
    public class Module
    {
        public string Name { get; set; }
        
        private string ModulePath { get { return Path.Combine(Directory.GetCurrentDirectory(), "modules", Name); } }
        
        public Module(string name)
        {
            Name = name;
        }

        public void CopyProject(string location)
        {
            foreach (string dirPath in Directory.GetDirectories(ModulePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(ModulePath, Path.Combine(location, "modules", Name)));

            foreach (string newPath in Directory.GetFiles(ModulePath, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(ModulePath, Path.Combine(location, "modules", Name)), true);
        }

        public void AddToProjectXML(XElement project)
        {
            AddReference(GetItemGroup(project, "Debug"), "Debug");
            AddReference(GetItemGroup(project, "Release"), "Release");
        }

        private XElement GetItemGroup(XElement project, string configuration)
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

        private void AddReference(XElement itemGroup, string configuration)
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

                referenceElement.Add(new XElement("HintPath", @$"..\typeo\modules\{Name}\{configuration}\{Name}.dll"));

                itemGroup.Add(referenceElement);
            }
        }
    }
}
