﻿using System;
using System.Threading.Tasks;

namespace TypeD.Models.Interfaces
{
    public interface ISaveModel
    {
        public void AddSave(string context, Func<Task> action);
        public Task Save();
        public bool AnythingToSave { get; }
    }
}
