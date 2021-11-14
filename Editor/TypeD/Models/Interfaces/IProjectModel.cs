using System.Threading.Tasks;
using TypeD.Models.Data;

namespace TypeD.Models.Interfaces
{
    public interface IProjectModel
    {
        public void AddModule(Project project, Module module);
        public Task<bool> Build(Project project);
        public void Run(Project project);
        public void AddCode(Project project, Codalyzer code);
        public void SetStartScene(Project project, TypeOType scene);
        public void BuildTypeOTypeTree(Project project);
        public bool LoadAssembly(Project project);
    }
}
