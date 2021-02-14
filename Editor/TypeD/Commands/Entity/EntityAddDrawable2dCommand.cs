using TypeD.Code;
using TypeD.Data;

namespace TypeD.Commands.Entity
{
    public partial class EntityCommand
    {
        public static void AddDrawable2d(TypeDType entity, TypeDType drawable2d)
        {
            if (entity?.TypeType != TypeDTypeType.Entity) return;
            if (drawable2d?.TypeType != TypeDTypeType.Drawable) return;
            var code = entity.Codes.Find(s => { return s is EntityTypeDCode; }) as EntityTypeDCode;
            code.Drawables.Add(drawable2d.FullName);
        }
    }
}
