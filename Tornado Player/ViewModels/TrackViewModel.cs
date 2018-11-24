namespace Tornado.Player.ViewModels
{
    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class TrackViewModel : ViewModelBase, ITrackViewModel
    {
        private readonly IWebService _webService;

        public TrackViewModel(IWebService webService)
        {
            _webService = webService;
        }

        public Track Track { get; private set; }

        public void Initialise(Track track)
        {
            Track = track;
        }

        public void FindOnYouTube()
        {
            _webService.YouTubeTrackQueryInBrowser(Track.Name);
        }
    }
}