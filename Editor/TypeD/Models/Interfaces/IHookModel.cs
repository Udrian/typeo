using System;

namespace TypeD.Models.Interfaces
{
    public interface IHookModel
    {
        public void AddHook(string hook, Action<object> action);
        public void Shoot(string hook, object param);
    }
}
