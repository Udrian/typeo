using System.Collections.Generic;
using TypeD.Models.Data;

namespace TypeD.Models.Interfaces
{
    public interface IRecentModel
    {
        public void Add(string projectFilePath, string projectName);
        public IList<Recent> Get();
    }
}
