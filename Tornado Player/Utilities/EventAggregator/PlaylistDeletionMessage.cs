namespace Tornado.Player.Utilities.EventAggregator
{
    using Tornado.Player.ViewModels.Interfaces.Playlist;

    internal class PlaylistDeletionMessage
    {
        internal PlaylistDeletionMessage(IPlaylistViewModel playlistViewModel)
        {
            PlaylistViewModel = playlistViewModel;
        }

        public IPlaylistViewModel PlaylistViewModel { get; }
    }
}