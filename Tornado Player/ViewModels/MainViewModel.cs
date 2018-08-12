namespace Tornado.Player.ViewModels
{
    using Tornado.Player.ViewModels.Interfaces;

    internal class MainViewModel : ViewModelBase, IMainViewModel
    {
        public MainViewModel(IPlaylistViewModel playlistViewModel)
        {
            PlaylistViewModel = playlistViewModel;
        }

        public IPlaylistViewModel PlaylistViewModel { get; }
    }
}