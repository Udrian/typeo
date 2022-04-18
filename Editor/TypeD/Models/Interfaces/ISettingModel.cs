using TypeD.Models.Data;

namespace TypeD.Models.Interfaces
{
    public interface ISettingModel : IModel
    {
        public void InitContext<T>() where T : SettingContext, new();
        public void RemoveContext<T>() where T : SettingContext, new();
        public T GetContext<T>(SettingLevel settingLevel = SettingLevel.System) where T : SettingContext, new();
        public void SetContext(SettingContext settingContext, SettingLevel? settingLevel = null);
    }
}
