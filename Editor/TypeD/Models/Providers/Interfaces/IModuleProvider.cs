﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TypeD.Models.Data;

namespace TypeD.Models.Providers.Interfaces
{
    public interface IModuleProvider
    {
        public Task<IEnumerable<ModuleList>> List();
        public Module Add(string name, string version);
    }
}