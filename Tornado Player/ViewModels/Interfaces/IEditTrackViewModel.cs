namespace Tornado.Player.ViewModels.Interfaces
{
    internal interface IEditTrackViewModel : IViewModelBase
    {
        ITrackViewModel Target { get; }

        bool IsSelected { get; }
    }
}