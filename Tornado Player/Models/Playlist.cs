namespace Tornado.Player.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    internal class Playlist : Snowflake
    {
        private static readonly Random Random = new Random();

        private readonly List<PlaylistTrack> _tracks;

        [JsonConstructor]
        internal Playlist(ulong id, string name, bool isShuffled, int selectedTrackIndex, IEnumerable<PlaylistTrack> tracks) : base(id)
        {
            Name = name;
            _isShuffled = isShuffled;
            SelectedTrackIndex = selectedTrackIndex;
            _tracks = new List<PlaylistTrack>(tracks);
        }

        internal Playlist(ulong id, string name, int selectedTrackIndex, IEnumerable<Track> tracks) : base(id)
        {
            Name = name;
            _isShuffled = false;
            SelectedTrackIndex = selectedTrackIndex;
            _tracks = new List<PlaylistTrack>(tracks.Select(track => new PlaylistTrack(0, track)));
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

        [JsonProperty("Tracks")]
        public IReadOnlyList<PlaylistTrack> Tracks => _tracks;

        internal void Load(Dictionary<ulong, Track> trackRepository)
        {
            foreach (PlaylistTrack track in _tracks)
            {
                track.Load(trackRepository);
            }
        }

        internal void AddTrack(ulong id, Dictionary<ulong, Track> trackRepository)
        {
            PlaylistTrack newTrack = new PlaylistTrack(sortOrder: IsShuffled ? Random.Next(0, _tracks.Count) : 0,
                                                       track: trackRepository[id]);

            _tracks.Add(newTrack);
        }

        internal void RemoveTrack(ulong id)
        {
            _tracks.Remove(_tracks.Single(playlistTrack => playlistTrack.Track.Id == id));
        }

        private void Sort()
        {
            foreach (PlaylistTrack track in Tracks)
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