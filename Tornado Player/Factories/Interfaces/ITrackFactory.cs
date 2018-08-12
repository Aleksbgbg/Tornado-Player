namespace Tornado.Player.Factories.Interfaces
{
    using Tornado.Player.Models;
    using Tornado.Player.ViewModels.Interfaces;

    internal interface ITrackFactory
    {
        ITrackViewModel MakeTrackViewModel(Track track);
    }
}