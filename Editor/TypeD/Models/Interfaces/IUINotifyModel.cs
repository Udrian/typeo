using System;
using System.Runtime.CompilerServices;
using TypeD.ViewModel;

namespace TypeD.Models.Interfaces
{
    public interface IUINotifyModel : IModel
    {
        public void Attach(string key, Action<string> notifyEvent);
        public void Attach<T>(Action<string> notifyEvent) where T : ViewModelBase;
        public void Detach(string key);
        public void Detach<T>() where T : ViewModelBase;
        public void Notify([CallerMemberName] string name = null);
        public void Notify(string key, [CallerMemberName] string name = null);
        public void Notify<T>([CallerMemberName] string name = null) where T : ViewModelBase;
    }
}
