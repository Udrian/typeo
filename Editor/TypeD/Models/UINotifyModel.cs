using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TypeD.Models.Interfaces;
using TypeD.ViewModel;

namespace TypeD.Models
{
    public class UINotifyModel : IUINotifyModel
    {
        // Data
        private Dictionary<string, Action<string>> Attachments { get; set; }

        // Constructors
        public UINotifyModel()
        {
            Attachments = new Dictionary<string, Action<string>>();
        }

        public void Init(IResourceModel resourceModel)
        {
        }

        // Functions
        public void Attach(string key, Action<string> notifyEvent)
        {
            if(!Attachments.ContainsKey(key))
                Attachments.Add(key, notifyEvent);
        }

        public void Attach<T>(Action<string> notifyEvent) where T : ViewModelBase
        {
            Attach(typeof(T).FullName, notifyEvent);
        }

        public void Detach(string key)
        {
            if (Attachments.ContainsKey(key))
                Attachments.Remove(key);
        }

        public void Detach<T>() where T : ViewModelBase
        {
            Detach(typeof(T).FullName);
        }

        public void Notify([CallerMemberName] string name = null)
        {
            foreach(var attachment in Attachments)
            {
                attachment.Value.Invoke(name);
            }
        }

        public void Notify(string key, [CallerMemberName] string name = null)
        {
            if (Attachments.ContainsKey(key))
                Attachments[key].Invoke(name);
        }

        public void Notify<T>([CallerMemberName] string name = null) where T : ViewModelBase
        {
            Notify(typeof(T).FullName);
        }
    }
}
