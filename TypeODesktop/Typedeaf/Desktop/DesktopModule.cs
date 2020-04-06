using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Desktop.Engine.Services;
using TypeOEngine.Typedeaf.Desktop.Engine.Services.Interfaces;

namespace TypeOEngine.Typedeaf.Desktop
{
    public class DesktopModule : Module<DesktopModuleOption>
    {
        public DesktopModule() : base(new Version(0, 1, 0))
        {
        }

        public override void Cleanup()
        {
        }

        public override void Initialize()
        {
            TypeO.RequireTypeO(new Version(0, 1, 0));

            if (TypeO.Context.Logger is DefaultLogger)
            {
                (TypeO.Context.Logger as DefaultLogger).LogToDisk = Option.SaveLogsToDisk;
                if (!string.IsNullOrEmpty(Option.LogPath))
                {
                    (TypeO.Context.Logger as DefaultLogger).LogPath = Option.LogPath;
                }
            }

        }

        public override void LoadExtensions()
        {
            TypeO.AddService<IWindowService, WindowService>();
            TypeO.AddService<IKeyboardInputService, KeyboardInputService>();
            TypeO.AddService<IMouseInputService, MouseInputService>();
        }
    }
}