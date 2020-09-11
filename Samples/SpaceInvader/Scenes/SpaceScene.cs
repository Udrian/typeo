using SpaceInvader.Entities;
using SpaceInvader.Logics.Scenes;
using TypeOEngine.Typedeaf.Core;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Entities.Drawables;
using TypeOEngine.Typedeaf.Core.Interfaces;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;

namespace SpaceInvader.Scenes
{
    class SpaceScene : Scene, IHasGame<SpaceInvaderGame>
    {
        public KeyboardInputService KeyboardInputService { get; set; }

        public SpaceInvaderGame Game { get; set; }
        public Font LoadedFont { get; set; }
        public Player Player { get; set; }
        public SpaceSpawnLogic SpaceSpawnLogic { get; set; }
        public bool PauseLogic { get; set; }

        private DrawableFont DrawableScore { get; set; }

        public override void Initialize()
        {
            LoadedFont = ContentLoader.LoadContent<Font>("content/Awesome.ttf");
            LoadedFont.FontSize = 48;

            DrawableScore = Drawables.Create(new DrawableFontOption() {
                Font = LoadedFont, 
                Position = new Vec2(15, 15), 
                Color = Color.Green
            });

            Entities.Create<Space>();

            Player = Entities.Create<Player>();

            SpaceSpawnLogic = Logics.Create<SpaceSpawnLogic>();
        }

        public override void Update(double dt)
        {
            Entities.Update(dt);
            UpdateLoop.Update(dt);
            if(KeyboardInputService.IsDown("Quit"))
            {
                Game.Exit();
            }
            
            DrawableScore.Text = $"Score: {Game.Score}";
        }

        public override void Draw()
        {
            Canvas.Clear(Color.Black);

            DrawStack.Draw(Canvas);

            DrawableScore.Draw(Canvas);

            for(int i = 0; i < Player.EntityData.Health; i++)
            {
                Canvas.DrawImage(Player.HealthTexture, new Vec2(Window.Size.X - (((Player.HealthTexture.Size.X * 0.5 + 15) * i) + 15 + Player.HealthTexture.Size.X), 25), scale: new Vec2(0.5));
            }

            Canvas.Present();
        }

        public override void OnEnter(Scene from)
        {
        }

        public override void OnExit(Scene to)
        {
            foreach(var bullet in Entities.List<Bullet>())
            {
                bullet.Remove();
            }
        }
    }
}
