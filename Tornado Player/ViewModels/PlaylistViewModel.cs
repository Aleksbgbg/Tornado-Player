namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;

    using Tornado.Player.Models;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaylistViewModel : ViewModelBase, IPlaylistViewModel
    {
        private readonly IMusicPlayerService _musicPlayerService;

        public PlaylistViewModel(IMusicPlayerService musicPlayerService)
        {
            _musicPlayerService = musicPlayerService;

            Tracks = new BindableCollection<Track>(_musicPlayerService.Tracks);

            _musicPlayerService.PlaylistLoaded += (sender, e) =>
            {
                Tracks.Clear();
                Tracks.AddRange(e.Tracks);
            };
        }

        public IObservableCollection<Track> Tracks { get; }
    }
}