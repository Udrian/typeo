using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Data.SaveContexts;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    internal class SettingModel : ISettingModel
    {
        // Data
        Dictionary<SettingLevel, List<SettingContext>> Contexts { get; set; }

        // Model
        ISaveModel SaveModel { get; set; }
        IHookModel HookModel { get; set; }

        // Constructors
        public SettingModel()
        {
            Contexts = new Dictionary<SettingLevel, List<SettingContext>>();
            Contexts.Add(SettingLevel.System, new List<SettingContext>());
            Contexts.Add(SettingLevel.Global, new List<SettingContext>());
            Contexts.Add(SettingLevel.Local, new List<SettingContext>());
        }

        public void Init(IResourceModel resourceModel)
        {
            SaveModel = resourceModel.Get<ISaveModel>();
            HookModel = resourceModel.Get<IHookModel>();

            HookModel.AddHook<ExitHook>((hook) =>
            {
                var settingSaveContext = SaveModel.GetSaveContext<SettingSaveContext>();
                settingSaveContext.Settings.Clear();

                foreach (var contexts in Contexts.Values)
                {
                    foreach(var context in contexts)
                    {
                        if (context.SaveOnExit && context.ShouldSave)
                        {
                            settingSaveContext.Settings.Add(context);
                        }
                    }
                }

                SaveModel.SaveNow<SettingSaveContext>();
            });
        }

        // Functions
        public void InitContext<T>() where T : SettingContext, new()
        {
            var paths = new List<string>() { SettingContext.SystemPath, SettingContext.GlobalPath, SettingContext.LocalPath };
            var i = 0;
            foreach(var path in paths)
            {
                var filePath = $"{path}/{GetName<T>()}.json";
                T context = null;
                if (File.Exists(filePath))
                {
                    context = new T().Merge(JSON.Deserialize<T>(filePath)) as T;
                }
                else
                {
                    context = new T();
                }
                context.Init((SettingLevel)i);
                Contexts[(SettingLevel)i].Add(context);
                i++;
            }
        }

        public void RemoveContext<T>() where T : SettingContext, new()
        {
            foreach(SettingLevel level in Enum.GetValues(typeof(SettingLevel)))
            {
                Contexts[level].RemoveAll(s => s.GetType() == typeof(T));
            }
        }

        public T GetContext<T>(SettingLevel settingLevel = SettingLevel.Local) where T : SettingContext, new()
        {
            T retVal = Contexts[SettingLevel.System].First(c => c.GetType() == typeof(T)) as T;

            for(int i = 1; i <= (int)settingLevel; i++)
            {
                retVal = retVal.Merge(Contexts[(SettingLevel)i].First(c => c.GetType() == typeof(T)) as T) as T;
            }

            retVal.Level = settingLevel;
            return retVal;
        }

        public void SetContext(SettingContext settingContext, SettingLevel? settingLevel = null)
        {
            if(settingLevel.HasValue)
            {
                settingContext.Level = settingLevel.Value;
            }
            var i = Contexts[settingContext.Level].FindIndex(s => s.GetType() == settingContext.GetType());
            Contexts[settingContext.Level][i] = settingContext;
            settingContext.ShouldSave = true;

            if (!settingContext.SaveOnExit)
            {
                var settingSaveContext = SaveModel.GetSaveContext<SettingSaveContext>();
                settingSaveContext.Settings.Add(settingContext);
                SaveModel.AddSave<SettingSaveContext>();
            }
        }

        // Internal
        internal static string GetName<T>() where T : SettingContext, new()
        {
            return GetName(typeof(T));
        }

        internal static string GetName(Type type)
        {
            var attribute = Attribute.GetCustomAttribute(type, typeof(NameAttribute)) as NameAttribute;
            if (attribute == null)
            {
                var name = type.Name;
                foreach (var item in new List<string>() { "SettingContext", "Setting", "Context" })
                {
                    if (name.EndsWith(item))
                    {
                        name = name.Substring(0, name.LastIndexOf(item));
                        break;
                    }
                }
                return name;
            }

            return attribute.Name;
        }
    }
}
