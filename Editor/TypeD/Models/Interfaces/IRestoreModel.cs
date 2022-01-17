﻿using System;
using System.Threading.Tasks;
using TypeD.Models.Data;

namespace TypeD.Models.Interfaces
{
    public interface IRestoreModel
    {
        public void AddRestoreMethod(Action<Project> restore);
        public Task Restore(Project project);
    }
}
