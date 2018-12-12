namespace Tornado.Player.Models
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    internal class Track : Snowflake, IComparable<Track>, IEquatable<Track>
    {
        [JsonConstructor]
        internal Track(ulong id, string filepath) : base(id)
        {
            Filepath = filepath;
        }

        [JsonProperty("Filepath")]
        public string Filepath { get; }

        [JsonIgnore]
        public string Name => Path.GetFileNameWithoutExtension(Filepath);

        public int CompareTo(Track other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public bool Equals(Track other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Track other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool MatchesSearch(string search)
        {
            return Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}