namespace Tornado.Player.ViewModels.Playlist
{
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models.Player;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Playlist;

    internal class ManagedPlaylistViewModel : PlaylistViewModel, IManagedPlaylistViewModel
    {
        private readonly IViewModelFactory _viewModelFactory;

        public ManagedPlaylistViewModel
        (
                IViewModelFactory viewModelFactory,
                IEventAggregator eventAggregator,
                IContentManagerService contentManagerService,
                IMusicPlayerService musicPlayerService,
                Playlist playlist
        )
                : base(viewModelFactory, eventAggregator, contentManagerService, musicPlayerService, playlist)
        {
            _viewModelFactory = viewModelFactory;
        }

        private protected override void OnPlayed()
        {
            Items.Clear();
            Items.AddRange(Playlist.Tracks.Select(playlistTrack => _viewModelFactory.MakeViewModel<ITrackViewModel>(playlistTrack)));
        }
    }
}