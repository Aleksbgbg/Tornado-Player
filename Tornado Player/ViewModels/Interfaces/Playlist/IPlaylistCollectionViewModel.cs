namespace Tornado.Player.ViewModels.Interfaces.Playlist
{
    using Caliburn.Micro;

    internal interface IPlaylistCollectionViewModel : IViewModelBase, IConductor
    {
        IObservableCollection<IPlaylistViewModel> Playlists { get; }
    }
}