namespace Tornado.Player.ViewModels.Interfaces
{
    using Tornado.Player.Models;

    internal interface ITrackViewModel : IViewModelBase
    {
        Track Track { get; }
    }
}