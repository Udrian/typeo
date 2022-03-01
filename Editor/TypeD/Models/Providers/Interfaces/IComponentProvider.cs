using System;
using System.Collections.Generic;
using TypeD.Code;
using TypeD.Models.Data;

namespace TypeD.Models.Providers.Interfaces
{
    public interface IComponentProvider
    {
        public void Create<T>(Project project, string className, string @namespace, Component parentComponent = null, List<string> interfaces = null) where T : ComponentTypeCode;
        public void Save(Project project, Component component);
        public Component Load(Project project, string fullName);
        public void Delete(Project project, Component component);
        public bool Exists(Project project, Component component);
        public bool Exists(Project project, Type type);
        public List<Component> ListAll(Project project);
    }
}
