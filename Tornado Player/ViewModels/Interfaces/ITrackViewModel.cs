namespace Tornado.Player.ViewModels.Interfaces
{
    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;

    internal interface ITrackViewModel : IViewModelBase
    {
        bool IsPlaying { get; }

        PlaylistTrack PlaylistTrack { get; }

        void Play();
    }
}