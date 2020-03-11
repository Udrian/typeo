namespace TypeOEngine.Typedeaf.Core
{
    namespace Entities.Interfaces
    {
        public interface IHasLogic
        {
            public bool PauseLogic { get; set; }
            public Logic Logic { get; set; }

            public void CreateLogic();
        }

        public interface IHasLogic<L> : IHasLogic where L : Logic, new()
        {
            Logic IHasLogic.Logic { get { return Logic; } set { Logic = value as L; } }
            public new L Logic { get; set; }

            void IHasLogic.CreateLogic()
            {
                Logic = new L();
            }
        }
    }
}
