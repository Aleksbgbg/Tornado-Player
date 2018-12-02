namespace Tornado.Player.ViewModels.Interfaces
{
    using Caliburn.Micro;

    internal interface IMainViewModel : IViewModelBase, IConductor
    {
        IPlaylistCollectionViewModel PlaylistCollectionViewModel { get; }
    }
}