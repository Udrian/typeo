using TypeD.Code;
using TypeD.Data;

namespace TypeD.Commands.Scene
{
    public partial class SceneCommand
    {
        public static void AddEntity(TypeDType scene, TypeDType entity)
        {
            if (scene.TypeType != TypeDTypeType.Scene) return;
            if (entity.TypeType != TypeDTypeType.Entity) return;
            var code = scene.Codes.Find(s => { return s is SceneTypeDCode; }) as SceneTypeDCode;
            code.Data.Entities.Add(entity.FullName);
        }
    }
}
