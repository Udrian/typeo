using TypeD.Types;

namespace TypeDCore.Code.Game
{
    class GameTypeOType : TypeOType
    {
        public override void Init()
        {
            Codes.Add(new GameCode(Project));
            Codes.Add(new GameTypeDCode(Project));
        }
    }
}
