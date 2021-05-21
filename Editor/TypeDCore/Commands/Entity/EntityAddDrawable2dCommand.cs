using TypeD.Data;
using TypeDCore.Code;

namespace TypeDCore.Commands.Entity
{
    public static partial class EntityCommand
    {
        public static void AddDrawable2d(this TypeD.Commands.Entity.EntityCommand _, TypeDType entity, TypeDType drawable2d)
        {
            if (entity?.TypeType != TypeDTypeType.Entity) return;
            if (drawable2d?.TypeType != TypeDTypeType.Drawable) return;
            var code = entity.Codes.Find(s => { return s is EntityTypeDCode; }) as EntityTypeDCode;
            code.Drawables.Add(drawable2d.FullName);
        }
    }
}
