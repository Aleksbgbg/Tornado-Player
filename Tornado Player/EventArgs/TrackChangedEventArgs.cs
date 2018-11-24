namespace Tornado.Player.EventArgs
{
    using System;
    using Tornado.Player.Models;

    internal class TrackChangedEventArgs : EventArgs
    {
        public TrackChangedEventArgs(int trackIndex, Track track, TimeSpan duration)
        {
            TrackIndex = trackIndex;
            Track = track;
            Duration = duration;
        }

        public int TrackIndex { get; }

        public Track Track { get; }

        public TimeSpan Duration { get; }
    }
}