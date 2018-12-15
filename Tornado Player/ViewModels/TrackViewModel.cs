namespace Tornado.Player.ViewModels
{
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class TrackViewModel : ViewModelBase, ITrackViewModel
    {
        private readonly IMusicPlayerService _musicPlayerService;

        private readonly IWebService _webService;

        public TrackViewModel(IMusicPlayerService musicPlayerService, IWebService webService, PlaylistTrack track)
        {
            _musicPlayerService = musicPlayerService;
            _webService = webService;

            Track = track;
        }

        public bool IsPlaying => _musicPlayerService.Track == Track.Track;

        public PlaylistTrack Track { get; }

        public void Play()
        {
            _musicPlayerService.SelectTrack(Track.Track);
        }

        public void FindOnYouTube()
        {
            _webService.YouTubeTrackQueryInBrowser(Track.Track.Name);
        }
    }
}