namespace Tornado.Player.ViewModels.Interfaces.Playlist
{
    using Caliburn.Micro;

    internal interface IPlaylistCollectionViewModel : IViewModelBase, IConductor, ITabViewModel
    {
        IObservableCollection<IPlaylistViewModel> Playlists { get; }
    }
}