using TypeD.Types;
using TypeDCore.Code.Entity;

namespace TypeDCore.Commands.Entity
{
    public static partial class EntityCommand
    {
        /*public static void AddDrawable2d(this TypeD.Commands.Entity.EntityCommand _, TypeOType entity, TypeOType drawable2d)
        {
            if (entity?.TypeOBaseType != "Entity") return;
            if (drawable2d?.TypeOBaseType != "Drawable") return;
            var code = entity.Codes.Find(s => { return s is EntityTypeDCode; }) as EntityTypeDCode;
            code.Drawables.Add(drawable2d.FullName);
        }*/
    }
}
