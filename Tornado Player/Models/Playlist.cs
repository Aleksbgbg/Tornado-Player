namespace Tornado.Player.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    internal class Playlist
    {
        [JsonConstructor]
        internal Playlist(ulong id, string name, IEnumerable<Track> tracks)
        {
            Id = id;
            Name = name;
            Tracks = new List<Track>(tracks);
        }

        [JsonProperty("Id")]
        public ulong Id { get; }

        [JsonProperty("Name")]
        public string Name { get; }

        [JsonProperty("Tracks")]
        public List<Track> Tracks { get; }
    }
}