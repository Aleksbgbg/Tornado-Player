namespace Tornado.Player.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    internal class Playlist : Snowflake
    {
        [JsonConstructor]
        internal Playlist(ulong id, string name, bool isShuffled, int selectedTrackIndex, IEnumerable<ulong> trackIds) : base(id)
        {
            Name = name;
            _isShuffled = isShuffled;
            SelectedTrackIndex = selectedTrackIndex;
            Tracks = new List<Track>(trackIds.Select(trackId => new Track(trackId)));
        }

        [JsonProperty("Name")]
        public string Name { get; }

        private bool _isShuffled;
        [JsonProperty("IsShuffled")]
        public bool IsShuffled
        {
            get => _isShuffled;

            set
            {
                if (_isShuffled == value) return;

                _isShuffled = value;
                NotifyOfPropertyChange(() => IsShuffled);

                if (IsShuffled)
                {
                    Sort();
                }
                else
                {
                    Shuffle();
                }
            }
        }

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

        private void Sort()
        {
            foreach (Track track in Tracks)
            {
                track.SortOrder = 0;
            }
        }

        private void Shuffle()
        {
            int[] sortOrders = new int[Tracks.Count];

            {
                Random random = new Random();

                for (int index = 0; index < sortOrders.Length; ++index)
                {
                    int shuffleIndex = random.Next(0, index + 1);

                    if (shuffleIndex != index)
                    {
                        sortOrders[index] = sortOrders[shuffleIndex];
                    }

                    // In this assignment, index is the next value from a function
                    // which generates the random numbers 0 .. sortOrders.Length in order
                    sortOrders[shuffleIndex] = index;
                }
            }

            for (int index = 0; index < sortOrders.Length; ++index)
            {
                Tracks[index].SortOrder = sortOrders[index];
            }
        }
    }
}