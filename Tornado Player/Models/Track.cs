namespace Tornado.Player.Models
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    internal class Track : Snowflake, IComparable, IComparable<Track>, IEquatable<Track>
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

        public static bool operator ==(Track left, Track right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Track left, Track right)
        {
            return !(left == right);
        }

        public int CompareTo(Track other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public int CompareTo(object other)
        {
            if (other is null)
            {
                return 1;
            }

            return CompareTo((Track)other);
        }

        public bool Equals(Track other)
        {
            if (other is null)
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