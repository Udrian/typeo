namespace TypeD.Models.Data.SettingContexts
{
    [Name("MainWindow")]
    public class MainWindowSettingContext : SettingContext
    {
        public Setting<int> SizeX { get; set; } = new Setting<int>(1024);
        public Setting<int> SizeY { get; set; } = new Setting<int>(768);
        public Setting<bool> Fullscreen { get; set; } = new Setting<bool>(true);

        public MainWindowSettingContext()
        {
            SaveOnExit = true;
        }
    }
}
