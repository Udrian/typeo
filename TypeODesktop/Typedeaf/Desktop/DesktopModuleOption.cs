using TypeOEngine.Typedeaf.Core.Engine;

namespace TypeOEngine.Typedeaf.Desktop
{
    public class DesktopModuleOption : ModuleOption
    {
        public bool SaveLogsToDisk { get; set; } = true;
        public string LogPath { get; set; } = null;
    }
}
