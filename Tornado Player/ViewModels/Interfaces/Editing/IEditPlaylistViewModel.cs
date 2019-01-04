namespace Tornado.Player.ViewModels.Interfaces.Editing
{
    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;
    using Tornado.Player.ViewModels.Interfaces.Playlist;

    internal interface IEditPlaylistViewModel : IViewModelBase
    {
        Playlist Playlist { get; }

        IPlaylistViewModel PlaylistViewModel { get; }
    }
}