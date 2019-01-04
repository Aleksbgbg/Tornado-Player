namespace Tornado.Player.ViewModels
{
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.Models.Player;
    using Tornado.Player.Services.Interfaces;
    using Tornado.Player.ViewModels.Interfaces;

    internal class CustomPlaylistViewModel : PlaylistViewModel, ICustomPlaylistViewModel
    {
        public CustomPlaylistViewModel
        (
                IViewModelFactory viewModelFactory,
                IEventAggregator eventAggregator,
                IContentManagerService contentManagerService,
                IMusicPlayerService musicPlayerService,
                Playlist playlist
        )
                : base(viewModelFactory, eventAggregator, contentManagerService, musicPlayerService, playlist)
        {
            Items.AddRange(playlist.Tracks.Select(track => viewModelFactory.MakeViewModel<ITrackViewModel>(track)));
        }
    }
}