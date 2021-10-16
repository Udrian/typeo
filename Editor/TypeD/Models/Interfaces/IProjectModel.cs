using System.Collections.Generic;
using System.Threading.Tasks;
using TypeD.Models.Data;
using TypeD.Types;

namespace TypeD.Models.Interfaces
{
    public interface IProjectModel
    {
        public void AddModule(Project project, Module module);
        public Task<bool> Build(Project project);
        public void Run(Project project);
        public void AddCode(Project project, Codalyzer code, string typeOBaseType = "");
        public List<TypeOType> GetTypeFromName(Project project, string name);
        public void SetStartScene(Project project, TypeOType scene);
        public void BuildTree(Project project);
    }
}
