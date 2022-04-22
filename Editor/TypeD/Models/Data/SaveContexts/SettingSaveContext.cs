using System.Collections.Generic;
using System.Threading.Tasks;
using TypeD.Helpers;
using TypeD.Models.Interfaces;

namespace TypeD.Models.Data.SaveContexts
{
    public class SettingSaveContext : SaveContext
    {

        // Properties
        public List<SettingContext> Settings { get; set; }

        // Constructors
        public override void Init(IResourceModel resourceModel, object param = null)
        {
            Settings = new List<SettingContext>();
        }

        // Functions
        public override Task SaveAction()
        {
            return Task.Run(() =>
            {
                foreach (var setting in Settings)
                {
                    if (!setting.ShouldSave) continue;

                    var paths = new List<string>() { SettingContext.SystemPath, SettingContext.GlobalPath, SettingContext.LocalPath };
                    var path = paths[(int)setting.Level];
                    var filePath = $"{path}/{SettingModel.GetName(setting.GetType())}.json";
                    JSON.Serialize(setting, filePath);
                    setting.ShouldSave = false;
                }
            });
        }
    }
}
