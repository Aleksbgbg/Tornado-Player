namespace Tornado.Player.EventArgs
{
    using System;

    internal class TrackChangedEventArgs : EventArgs
    {
        public TrackChangedEventArgs(int trackIndex, TimeSpan duration)
        {
            TrackIndex = trackIndex;
            Duration = duration;
        }

        public int TrackIndex { get; }

        public TimeSpan Duration { get; }
    }
}