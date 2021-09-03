using System;
using System.Collections.Generic;
using System.IO;
using TypeD.Helpers;

namespace TypeD.Models
{
    public class RecentModel
    {
        public static string RecentFilePath { get { return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TypeO", "recent"); } }
        public static int RecentLength { get; set; } = 5;

        public static void SaveRecents(string projectFilePath, string projectName)
        {
            var recents = JSON.Deserialize<List<RecentModel>>(RecentFilePath) ?? new List<RecentModel>();

            recents.RemoveAll((recent) => { return recent.Path == projectFilePath || !File.Exists(recent.Path); });
            recents.Insert(0, new RecentModel() { Name = projectName, Path = projectFilePath, DateTime = DateTime.Now });

            if (recents.Count > RecentLength)
            {
                recents.RemoveRange(RecentLength, recents.Count - RecentLength);
            }
            JSON.Serialize(recents, RecentFilePath);
        }

        public static List<RecentModel> LoadRecents()
        {
            var recents = JSON.Deserialize<List<RecentModel>>(RecentFilePath) ?? new List<RecentModel>();
            recents.RemoveAll((recent) => { return !File.Exists(recent.Path); });
            return recents;
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime DateTime { get; set; }
    }
}
