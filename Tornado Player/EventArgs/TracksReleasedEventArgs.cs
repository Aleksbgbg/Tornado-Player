namespace Tornado.Player.EventArgs
{
    using System.Collections.Generic;

    using Tornado.Player.ViewModels.Interfaces;

    internal class TracksReleasedEventArgs
    {
        public TracksReleasedEventArgs(IEnumerable<ITrackViewModel> tracks)
        {
            Tracks = tracks;
        }

        public IEnumerable<ITrackViewModel> Tracks { get; }
    }
}