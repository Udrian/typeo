using TypeD.Types;

namespace TypeDCore.Code.Scene
{
    class SceneTypeOType : TypeOType
    {
        public override void Init()
        {
            Codes.Add(new SceneCode(Project, ClassName, Namespace));
            Codes.Add(new SceneTypeDCode(Project, ClassName, Namespace));
        }
    }
}
