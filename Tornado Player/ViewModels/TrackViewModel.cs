namespace Tornado.Player.ViewModels
{
    using Tornado.Player.Models.Player;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class TrackViewModel : ViewModelBase, ITrackViewModel
    {
        private readonly IContentManagerService _contentManagerService;

        private readonly IMusicPlayerService _musicPlayerService;

        private readonly IWebService _webService;

        public TrackViewModel(IContentManagerService contentManagerService, IMusicPlayerService musicPlayerService, IWebService webService, PlaylistTrack playlistTrack)
        {
            _contentManagerService = contentManagerService;
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

        public void ToggleFavourite()
        {
            if (PlaylistTrack.Track.IsFavourite)
            {
                _contentManagerService.UnFavourite(PlaylistTrack.Track);
            }
            else
            {
                _contentManagerService.Favourite(PlaylistTrack.Track);
            }
        }

        public override object GetView(object context = default)
        {
            return null;
        }
    }
}