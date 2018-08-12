namespace Tornado.Player.ViewModels
{
    using Tornado.Player.Models;
    using Tornado.Player.ViewModels.Interfaces;

    internal class TrackViewModel : ViewModelBase, ITrackViewModel
    {
        public Track Track { get; private set; }

        public void Initialise(Track track)
        {
            Track = track;
        }
    }
}