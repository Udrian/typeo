using System.Threading.Tasks;
using TypeD.Models;

namespace TypeD.Commands.Project
{
    public static partial class ProjectCommand
    {
        public static async Task Save(ProjectModel project)
        {
            await project.Save();
        }
    }
}