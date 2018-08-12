namespace Tornado.Player.EventArgs
{
    using System;

    using Tornado.Player.Models;

    internal class PlaylistLoadedEventArgs : EventArgs
    {
        public PlaylistLoadedEventArgs(Track[] tracks)
        {
            Tracks = tracks;
        }

        public Track[] Tracks { get; }
    }
}