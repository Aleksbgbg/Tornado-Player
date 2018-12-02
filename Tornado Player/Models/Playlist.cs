namespace Tornado.Player.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    internal class Playlist : Snowflake
    {
        private static readonly Random Random = new Random();

        private readonly List<Track> _tracks;

        [JsonConstructor]
        internal Playlist(ulong id, string name, bool isShuffled, int selectedTrackIndex, IEnumerable<ulong> trackIds) : base(id)
        {
            Name = name;
            _isShuffled = isShuffled;
            SelectedTrackIndex = selectedTrackIndex;
            _tracks = new List<Track>(trackIds.Select(trackId => new Track(trackId)));
        }

        internal Playlist(ulong id, string name, bool isShuffled, int selectedTrackIndex, IEnumerable<Track> tracks) : base(id)
        {
            Name = name;
            _isShuffled = isShuffled;
            SelectedTrackIndex = selectedTrackIndex;
            _tracks = new List<Track>(tracks);
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
        public IReadOnlyList<Track> Tracks => _tracks;

        [JsonProperty("TrackIds")]
        private IEnumerable<ulong> TrackIds => Tracks.Select(track => track.Id);

        internal void Load(Dictionary<ulong, Track> trackRepository)
        {
            Track[] newTracks = _tracks.Select(track => trackRepository[track.Id]).ToArray();

            _tracks.Clear();
            _tracks.AddRange(newTracks);
        }

        internal void AddTrack(ulong id, Dictionary<ulong, Track> trackRepository)
        {
            Track newTrack = trackRepository[id];

            newTrack.SortOrder = IsShuffled ? Random.Next(0, _tracks.Count) : 0;

            _tracks.Add(newTrack);
        }

        internal void RemoveTrack(Track track)
        {
            _tracks.Remove(track);
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
                for (int index = 0; index < sortOrders.Length; ++index)
                {
                    int shuffleIndex = Random.Next(0, index + 1);

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
                _tracks[index].SortOrder = sortOrders[index];
            }
        }
    }
}