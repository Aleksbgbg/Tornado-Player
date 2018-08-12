namespace Tornado.Player.Factories
{
    using Caliburn.Micro;

    using Tornado.Player.Factories.Interfaces;
    using Tornado.Player.Models;
    using Tornado.Player.ViewModels.Interfaces;

    internal class TrackFactory : ITrackFactory
    {
        public ITrackViewModel MakeTrackViewModel(Track track)
        {
            ITrackViewModel trackViewModel = IoC.Get<ITrackViewModel>();
            trackViewModel.Initialise(track);

            return trackViewModel;
        }
    }
}