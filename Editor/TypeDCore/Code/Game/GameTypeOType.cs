using System.Reflection;
using TypeD.Models;
using TypeD.Types;

namespace TypeDCore.Code.Game
{
    class GameTypeOType : TypeOType
    {
        public GameTypeOType(string className, string @namespace, string typeOBaseType, TypeInfo typeInfo, ProjectModel project) : base(className, @namespace, typeOBaseType, typeInfo, project)
        {
            Codes.Add(new GameCode(Project));
            Codes.Add(new GameTypeDCode(Project));
        }
    }
}
