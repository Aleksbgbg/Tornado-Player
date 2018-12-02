namespace Tornado.Player.ViewModels
{
    using Caliburn.Micro;

    using Tornado.Player.ViewModels.Interfaces;

    internal sealed class MainViewModel : Conductor<IViewModelBase>.Collection.AllActive, IMainViewModel
    {
        public MainViewModel(IPlaylistCollectionViewModel playlistCollectionViewModel, IPlaybarViewModel playbarViewModel)
        {
            PlaylistCollectionViewModel = playlistCollectionViewModel;
            PlaybarViewModel = playbarViewModel;

            ActivateItem(PlaylistCollectionViewModel);
            ActivateItem(PlaybarViewModel);
        }

        public IPlaylistCollectionViewModel PlaylistCollectionViewModel { get; }

        public IPlaybarViewModel PlaybarViewModel { get; }
    }
}