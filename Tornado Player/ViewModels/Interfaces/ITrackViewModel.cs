namespace Tornado.Player.ViewModels.Interfaces
{
    using Tornado.Player.Models;

    internal interface ITrackViewModel : IViewModelBase
    {
        bool IsPlaying { get; }

        PlaylistTrack Track { get; }

        void Play();
    }
}