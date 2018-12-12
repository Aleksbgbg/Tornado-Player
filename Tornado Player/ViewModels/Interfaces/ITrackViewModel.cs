namespace Tornado.Player.ViewModels.Interfaces
{
    using Tornado.Player.Models;

    internal interface ITrackViewModel : IViewModelBase
    {
        PlaylistTrack Track { get; }
    }
}