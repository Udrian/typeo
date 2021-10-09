using System.Collections.Generic;
using TypeD.Models.Data;

namespace TypeD.Models.Providers.Interfaces
{
    public interface IRecentProvider
    {
        public void Add(string projectFilePath, string projectName);
        public IList<Recent> Get();
    }
}
