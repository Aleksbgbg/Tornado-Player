namespace Tornado.Player.ViewModels
{
    using Tornado.Player.ViewModels.Interfaces;

    internal class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel(IPlaylistViewModel playlistViewModel, IPlaybarViewModel playbarViewModel)
        {
            PlaylistViewModel = playlistViewModel;
            PlaybarViewModel = playbarViewModel;
        }

        public IPlaylistViewModel PlaylistViewModel { get; }

        public IPlaybarViewModel PlaybarViewModel { get; }
    }
}