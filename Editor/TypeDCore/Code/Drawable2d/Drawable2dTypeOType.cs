using TypeD.Types;

namespace TypeDCore.Code.Drawable2d
{
    class Drawable2dTypeOType : TypeOType
    {
        public override void Init()
        {
            Codes.Add(new Drawable2dCode(Project, ClassName, Namespace));
        }
    }
}
