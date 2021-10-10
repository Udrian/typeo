using TypeD.Types;
using TypeDCore.Code.Drawable2d;
using TypeDCore.Code.Entity;
using TypeDCore.Code.Game;
using TypeDCore.Code.Scene;

namespace TypeDCore.Code
{
    //TODO: Remove this
    public static class TempInit
    {
        public static void Init()
        {
            TypeOType.AddTypeOType(typeof(TypeOEngine.Typedeaf.Core.Game)                       , typeof(GameTypeOType)); 
            TypeOType.AddTypeOType(typeof(TypeOEngine.Typedeaf.Core.Scene)                      , typeof(SceneTypeOType));
            TypeOType.AddTypeOType(typeof(TypeOEngine.Typedeaf.Core.Entities.Entity)            , typeof(EntityTypeOType));
            TypeOType.AddTypeOType(typeof(TypeOEngine.Typedeaf.Core.Entities.Drawables.Drawable), typeof(Drawable2dTypeOType));
            //TypeOType.AddTypeOType(typeof(Stub), typeof());
            //TypeOType.AddTypeOType(typeof(Logic), typeof());
            //TypeOType.AddTypeOType(typeof(EntityData), typeof());

            //ProjectCommand.InitProject = ProjectCreateCommand;
        }
    }
}
