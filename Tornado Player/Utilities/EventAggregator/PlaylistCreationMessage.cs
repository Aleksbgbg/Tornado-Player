namespace Tornado.Player.Utilities.EventAggregator
{
    using Tornado.Player.ViewModels.Interfaces;
    using Tornado.Player.ViewModels.Interfaces.Playlist;

    internal class PlaylistCreationMessage
    {
        public PlaylistCreationMessage(IPlaylistViewModel playlistViewModel)
        {
            PlaylistViewModel = playlistViewModel;
        }

        public IPlaylistViewModel PlaylistViewModel { get; }
    }
}