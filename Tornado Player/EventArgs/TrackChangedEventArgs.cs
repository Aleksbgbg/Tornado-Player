namespace Tornado.Player.EventArgs
{
    using System;

    using Tornado.Player.Models.Player;

    public class TrackChangedEventArgs : EventArgs
    {
        public TrackChangedEventArgs(Track track, TimeSpan duration)
        {
            Track = track;
            Duration = duration;
        }

        public Track Track { get; }

        public TimeSpan Duration { get; }
    }
}