namespace Tornado.Player.Models
{
    using Caliburn.Micro;

    using Newtonsoft.Json;

    internal class AppLayout : PropertyChangedBase
    {
        [JsonConstructor]
        public AppLayout(bool showPlaylists)
        {
            _showPlaylists = showPlaylists;
        }

        private bool _showPlaylists;
        [JsonProperty("ShowPlaylists")]
        public bool ShowPlaylists
        {
            get => _showPlaylists;

            set
            {
                if (_showPlaylists == value) return;

                _showPlaylists = value;
                NotifyOfPropertyChange(() => ShowPlaylists);
            }
        }
    }
}