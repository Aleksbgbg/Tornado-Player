namespace Tornado.Player.ViewModels.Interfaces
{
    internal interface IMainViewModel : IViewModelBase
    {
        IPlaylistCollectionViewModel PlaylistCollectionViewModel { get; }
    }
}