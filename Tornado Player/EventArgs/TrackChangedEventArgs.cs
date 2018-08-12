namespace Tornado.Player.EventArgs
{
    using System;

    internal class TrackChangedEventArgs : EventArgs
    {
        public TrackChangedEventArgs(int trackIndex)
        {
            TrackIndex = trackIndex;
        }

        public int TrackIndex { get; }
    }
}