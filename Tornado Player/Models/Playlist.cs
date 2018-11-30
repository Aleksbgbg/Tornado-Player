namespace Tornado.Player.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    internal class Playlist : Snowflake
    {
        [JsonConstructor]
        internal Playlist(ulong id, string name, IEnumerable<Track> tracks) : base(id)
        {
            Name = name;
            Tracks = new List<Track>(tracks);
        }

        [JsonProperty("Name")]
        public string Name { get; }

        [JsonProperty("Tracks")]
        public List<Track> Tracks { get; }
    }
}