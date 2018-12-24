namespace Tornado.Player.ViewModels.Interfaces.Editing
{
    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;

    internal interface IEditPlaylistViewModel : IViewModelBase
    {
        Playlist Playlist { get; }

        IPlaylistViewModel PlaylistViewModel { get; }
    }
}