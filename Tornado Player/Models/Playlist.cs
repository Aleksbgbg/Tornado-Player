namespace Tornado.Player.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    internal class Playlist : Snowflake
    {
        [JsonConstructor]
        internal Playlist(ulong id, string name, bool isShuffled, int selectedTrackIndex, IEnumerable<ulong> trackIds) : base(id)
        {
            Name = name;
            IsShuffled = isShuffled;
            SelectedTrackIndex = selectedTrackIndex;
            Tracks = new List<Track>(trackIds.Select(trackId => new Track(trackId)));
        }

        [JsonProperty("Name")]
        public string Name { get; }

        [JsonProperty("IsShuffled")]
        public bool IsShuffled { get; set; }

        [JsonProperty("SelectedTrackIndex")]
        public int SelectedTrackIndex { get; set; }

        [JsonIgnore]
        public List<Track> Tracks { get; }

        [JsonProperty("TrackIds")]
        private IEnumerable<ulong> TrackIds => Tracks.Select(track => track.Id);

        public void Load(Dictionary<ulong, Track> trackRepository)
        {
            Track[] newTracks = Tracks.Select(track => trackRepository[track.Id]).ToArray();

            Tracks.Clear();
            Tracks.AddRange(newTracks);
        }
    }
}