namespace Tornado.Player.ViewModels
{
    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class TrackViewModel : ViewModelBase, ITrackViewModel
    {
        private readonly IMusicPlayerService _musicPlayerService;

        private readonly IWebService _webService;

        public TrackViewModel(IMusicPlayerService musicPlayerService, IWebService webService, PlaylistTrack playlistTrack)
        {
            _musicPlayerService = musicPlayerService;
            _webService = webService;

            PlaylistTrack = playlistTrack;
        }

        public bool IsPlaying => _musicPlayerService.Track == PlaylistTrack.Track;

        public PlaylistTrack PlaylistTrack { get; }

        public void Play()
        {
            _musicPlayerService.SelectTrack(PlaylistTrack.Track);
        }

        public void FindOnYouTube()
        {
            _webService.YouTubeTrackQueryInBrowser(PlaylistTrack.Track.Name);
        }

        public override object GetView(object context = default)
        {
            return null;
        }
    }
}