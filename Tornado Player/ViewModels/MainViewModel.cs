namespace Tornado.Player.ViewModels
{
    using Tornado.Player.ViewModels.Interfaces;

    internal class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel(IPlaylistCollectionViewModel playlistCollectionViewModel, IPlaybarViewModel playbarViewModel)
        {
            PlaylistCollectionViewModel = playlistCollectionViewModel;
            PlaybarViewModel = playbarViewModel;
        }

        public IPlaylistCollectionViewModel PlaylistCollectionViewModel { get; }

        public IPlaybarViewModel PlaybarViewModel { get; }
    }
}