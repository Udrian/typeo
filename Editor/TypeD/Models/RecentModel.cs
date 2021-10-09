using System;
using System.IO;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class RecentModel : IRecentModel
    {
        internal string RecentFilePath { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TypeO", "recent"); } }
        internal int RecentLength { get { return 5; } }

        public RecentModel()
        {
        }
    }
}
