namespace Tornado.Player.Utilities.EventAggregator
{
    using Tornado.Player.ViewModels.Interfaces;

    internal class PlaylistDeletionMessage
    {
        internal PlaylistDeletionMessage(IPlaylistViewModel playlistViewModel)
        {
            PlaylistViewModel = playlistViewModel;
        }

        public IPlaylistViewModel PlaylistViewModel { get; }
    }
}