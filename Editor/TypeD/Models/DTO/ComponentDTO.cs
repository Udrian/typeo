using System.Collections.Generic;

namespace TypeD.Models.DTO
{
    public class ComponentDTO
    {
        public string ClassName { get; set; }
        public string Namespace { get; set; }
        public List<string> TemplateClass { get; set; }
        public string ParentComponent { get; set; }
        public List<string> Interfaces { get; set; }
        public string TypeOBaseType { get; set; }
    }
}
