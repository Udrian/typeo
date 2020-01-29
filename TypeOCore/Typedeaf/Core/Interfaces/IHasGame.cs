namespace TypeOEngine.Typedeaf.Core
{
    namespace Interfaces
    {
        public interface IHasGame
        {
            public Game Game { get; set; }

            public void SetGame(Game game)
            {
                Game = game;
            }
        }

        public interface IHasGame<G> : IHasGame where G : Game
        {
            Game IHasGame.Game { get => Game; set => Game = (G)value; }
            public new G Game { get; set; }

            public void SetGame(G game)
            {
                (this as IHasGame).SetGame(game);
            }
        }
    }
}
