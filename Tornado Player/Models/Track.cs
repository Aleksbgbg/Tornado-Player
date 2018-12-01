namespace Tornado.Player.Models
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    internal class Track : Snowflake, IComparable<Track>
    {
        internal Track(ulong id) : base(id)
        {
        }

        [JsonConstructor]
        internal Track(ulong id, int sortOrder, string filepath) : base(id)
        {
            _sortOrder = sortOrder;
            Filepath = filepath;
        }

        private int _sortOrder;
        [JsonProperty("SortOrder")]
        public int SortOrder
        {
            get => _sortOrder;

            set
            {
                if (_sortOrder == value) return;

                _sortOrder = value;
                NotifyOfPropertyChange(() => SortOrder);
            }
        }

        [JsonProperty("Filepath")]
        public string Filepath { get; }

        [JsonIgnore]
        public string Name => Path.GetFileNameWithoutExtension(Filepath);

        public int CompareTo(Track other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public bool MatchesSearch(string search)
        {
            return Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}