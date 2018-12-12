namespace Tornado.Player.Models
{
    using System;
    using System.Collections.Generic;

    using Caliburn.Micro;

    using Newtonsoft.Json;

    internal class PlaylistTrack : PropertyChangedBase, IComparable<PlaylistTrack> // Track with sort order
    {
        internal PlaylistTrack(int sortOrder, Track track)
        {
            _sortOrder = sortOrder;
            Track = track;
            TrackId = track.Id;
        }

        [JsonConstructor]
        internal PlaylistTrack(int sortOrder, ulong trackId)
        {
            _sortOrder = sortOrder;
            TrackId = trackId;
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

        [JsonIgnore]
        public Track Track { get; private set; }

        [JsonProperty("TrackId")]
        private ulong TrackId { get; }

        public int CompareTo(PlaylistTrack other)
        {
            int sortOrderCompare = SortOrder.CompareTo(other.SortOrder);

            if (sortOrderCompare == 0)
            {
                return Track.CompareTo(other.Track);
            }

            return sortOrderCompare;
        }

        internal void Load(IDictionary<ulong, Track> trackRepository)
        {
            if (Track != null)
            {
                throw new InvalidOperationException("Track already loaded");
            }

            Track = trackRepository[TrackId];
        }
    }
}