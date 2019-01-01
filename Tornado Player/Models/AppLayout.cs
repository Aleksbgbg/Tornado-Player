namespace Tornado.Player.Models
{
    using Caliburn.Micro;

    using Newtonsoft.Json;

    internal class AppLayout : PropertyChangedBase
    {
        [JsonConstructor]
        public AppLayout(bool showPlaylists, double playlistsBoxColumnWidth)
        {
            _showPlaylists = showPlaylists;
            _playlistsBoxColumnWidth = playlistsBoxColumnWidth;
        }

        private bool _showPlaylists;
        [JsonProperty(nameof(ShowPlaylists))]
        public bool ShowPlaylists
        {
            get => _showPlaylists;

            set
            {
                if (_showPlaylists == value) return;

                _showPlaylists = value;
                NotifyOfPropertyChange(nameof(ShowPlaylists));
            }
        }

        private double _playlistsBoxColumnWidth;
        [JsonProperty(nameof(PlaylistsBoxColumnWidth))]
        public double PlaylistsBoxColumnWidth
        {
            get => _playlistsBoxColumnWidth;

            set
            {
                _playlistsBoxColumnWidth = value;
                NotifyOfPropertyChange(nameof(PlaylistsBoxColumnWidth));
            }
        }
    }
}