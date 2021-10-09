using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TypeD.Helpers;
using TypeD.Models.Data;
using TypeD.Models.Interfaces;

namespace TypeD.Models
{
    public class RecentModel : IRecentModel
    {
        public ObservableCollection<Recent> Recents { get; set; }
        protected string RecentFilePath { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TypeO", "recent"); } }
        protected int RecentLength { get { return 5; } }

        public RecentModel()
        {
            Recents = new ObservableCollection<Recent>();
            Load();
        }

        public void Add(string projectFilePath, string projectName)
        {
            for(int i = Recents.Count - 1; i >= 0; i--)
            {
                var recent = Recents[i];
                if(recent.Path == projectFilePath || !File.Exists(recent.Path))
                {
                    Recents.RemoveAt(i);
                }
            }
            Recents.Insert(0, new Recent() { Name = projectName, Path = projectFilePath, DateTime = DateTime.Now });

            if (Recents.Count > RecentLength)
            {
                for(int i = RecentLength; i < Recents.Count - RecentLength; i++)
                {
                    Recents.RemoveAt(i);
                }
            }

            Save();
        }

        public IList<Recent> Get()
        {
            return Recents;
        }

        private void Load()
        {
            Recents.Clear();
            var recents = JSON.Deserialize<List<Recent>>(RecentFilePath) ?? new List<Recent>();
            recents.RemoveAll((recent) => { return !File.Exists(recent.Path); });

            recents.ForEach(x => Recents.Add(x));
        }

        private void Save()
        {
            JSON.Serialize(Recents, RecentFilePath);
        }
    }
}
