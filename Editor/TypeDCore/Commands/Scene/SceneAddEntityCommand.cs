using TypeD.Data;
using TypeDCore.Code;

namespace TypeDCore.Commands.Scene
{
    public static partial class SceneCommand
    {
        public static void AddEntity(this TypeD.Commands.Scene.SceneCommand _, TypeDType scene, TypeDType entity)
        {
            if (scene?.TypeType != TypeDTypeType.Scene) return;
            if (entity?.TypeType != TypeDTypeType.Entity) return;
            var code = scene.Codes.Find(s => { return s is SceneTypeDCode; }) as SceneTypeDCode;
            code.Entities.Add(entity.FullName);
        }
    }
}
