using System;
using System.Threading.Tasks;
using TypeD.Models.Data;

namespace TypeD.Models.Providers.Interfaces
{
    public interface IProjectProvider
    {
        public Task<Project> Create(string projectName, string location, string csSolutionPath, string csProjName, Action<int> progress);
        public Task<Project> Load(string projectFilePath);
        public Task Save(Project project);
    }
}
