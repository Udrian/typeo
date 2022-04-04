using System;
using System.Runtime.CompilerServices;

namespace TypeD.Models.Interfaces
{
    public interface IUINotifyModel : IModel
    {
        public void Attach(string key, Action<string> notifyEvent);
        public void Detach(string key);
        public void Notify([CallerMemberName] string name = null);
    }
}
