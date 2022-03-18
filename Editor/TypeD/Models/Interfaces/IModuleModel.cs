using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using TypeD.Models.Data;

namespace TypeD.Models.Interfaces
{
    public interface IModuleModel
    {
        public Task<bool> Download(Module module, Action<long, int, long> progress);
        public void LoadAssembly(Module module);
        public void AddToProjectXML(Module module, XElement project);
    }
}
