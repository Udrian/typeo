using System;
using System.IO;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class RecentModel : IRecentModel, IModelProvider
    {
        internal string RecentFilePath { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TypeO", "recent"); } }
        internal int RecentLength { get { return 5; } }

        // Constructors
        public RecentModel()
        {
        }

        public void Init(IResourceModel resourceModel)
        {
        }
    }
}
