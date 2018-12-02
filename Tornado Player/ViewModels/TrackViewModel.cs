namespace Tornado.Player.ViewModels
{
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class TrackViewModel : ViewModelBase, ITrackViewModel
    {
        private readonly IMusicPlayerService _musicPlayerService;

        private readonly IWebService _webService;

        public TrackViewModel(IMusicPlayerService musicPlayerService, IWebService webService, Track track)
        {
            _musicPlayerService = musicPlayerService;
            _webService = webService;

            Track = track;
        }

        public Track Track { get; }

        protected override void OnActivate()
        {
            _musicPlayerService.SelectTrack(Track);
        }

        public void FindOnYouTube()
        {
            _webService.YouTubeTrackQueryInBrowser(Track.Name);
        }
    }
}