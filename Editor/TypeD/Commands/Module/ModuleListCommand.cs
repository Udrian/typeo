using System.Threading.Tasks;
using TypeD.Data;
using TypeD.Helpers;

namespace TypeD.Commands.Module
{
    public partial class ModuleCommand
    {
        public static async Task<ModuleListData> List()
        {
            var data = await APICaller.GetJsonObject<ModuleListData>("module/list");
            return data;
        }
    }
}
