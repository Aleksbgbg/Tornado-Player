namespace Tornado.Player.ViewModels.Interfaces.Editing
{
    using Tornado.Player.Models;

    internal interface IEditPlaylistViewModel : IViewModelBase
    {
        Playlist Playlist { get; }

        IPlaylistViewModel PlaylistViewModel { get; }
    }
}