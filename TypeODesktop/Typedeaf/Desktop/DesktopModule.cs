using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;

namespace TypeOEngine.Typedeaf.Desktop
{
    public class DesktopModule : Module<DesktopModuleOption>
    {
        public DesktopModule() : base()
        {
        }

        public override void Cleanup()
        {
        }

        public override void Initialize()
        {
            if(TypeO.Context.Logger is DefaultLogger)
            {
                (TypeO.Context.Logger as DefaultLogger).LogToDisk = Option.SaveLogsToDisk;
                if(!string.IsNullOrEmpty(Option.LogPath))
                {
                    (TypeO.Context.Logger as DefaultLogger).LogPath = Option.LogPath;
                }
            }

        }

        public override void LoadExtensions()
        {
            TypeO.AddService<WindowService>();
            TypeO.AddService<KeyboardInputService>();
            TypeO.AddService<MouseInputService>();
        }
    }
}