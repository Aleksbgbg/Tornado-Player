namespace Tornado.Player.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    internal class Playlist : Snowflake
    {
        [JsonConstructor]
        internal Playlist(ulong id, string name, bool isShuffled, IEnumerable<Track> tracks) : base(id)
        {
            Name = name;
            IsShuffled = isShuffled;
            Tracks = new List<Track>(tracks);
        }

        [JsonProperty("Name")]
        public string Name { get; }

        [JsonProperty("IsShuffled")]
        public bool IsShuffled { get; set; }

        [JsonProperty("Tracks")]
        public List<Track> Tracks { get; }
    }
}