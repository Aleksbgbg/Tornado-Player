namespace Tornado.Player.ViewModels
{
    using System.Linq;

    using Caliburn.Micro;
    using Caliburn.Micro.Wrapper;

    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaylistEditorViewModel : Conductor<IEditPlaylistViewModel>.Collection.OneActive, IPlaylistEditorViewModel
    {
        public PlaylistEditorViewModel(IViewModelFactory viewModelFactory, IPlaylistCollectionViewModel playlistCollectionViewModel)
        {
            Items.AddRange(playlistCollectionViewModel.Playlists.Select(playlist => viewModelFactory.MakeViewModel<IEditPlaylistViewModel>(playlist)));
        }
    }
}