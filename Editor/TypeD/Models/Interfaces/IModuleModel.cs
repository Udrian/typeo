using System.Threading.Tasks;
using TypeD.Models.Data;

namespace TypeD.Models.Interfaces
{
    public interface IModuleModel
    {
        public Task<bool> Download(Module module);
    }
}
