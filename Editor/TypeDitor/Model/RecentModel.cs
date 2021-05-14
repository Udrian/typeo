using System.Collections.Generic;
using System.IO;
using TypeD.Helpers;

namespace TypeDEditor.Model
{
    class RecentModel
    {
        public static string RecentFilePath { get; set; } = "recent";
        public static int RecentLength { get; set; } = 5;

        public static void SaveRecent(string projectFilePath, string projectName)
        {
            var recents = JSON.Deserialize<List<RecentModel>>(RecentFilePath) ?? new List<RecentModel>();

            recents.RemoveAll((recent) => { return recent.Path == projectFilePath || !File.Exists(recent.Path); });
            recents.Insert(0, new RecentModel() { Name = projectName, Path = projectFilePath });

            if (recents.Count > RecentLength)
            {
                recents.RemoveRange(RecentLength, recents.Count - RecentLength);
            }
            JSON.Serialize(recents, RecentFilePath);
        }

        public string Name { get; set; }
        public string Path { get; set; }
    }
}
