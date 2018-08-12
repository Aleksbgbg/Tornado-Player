namespace Tornado.Player.ViewModels.Interfaces
{
    internal interface IMainViewModel : IViewModelBase
    {
        IPlaylistViewModel PlaylistViewModel { get; }
    }
}