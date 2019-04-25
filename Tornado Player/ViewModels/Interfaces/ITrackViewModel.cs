namespace Tornado.Player.ViewModels.Interfaces
{
    using Tornado.Player.Models.Player;

    public interface ITrackViewModel : IViewModelBase
    {
        bool IsPlaying { get; }

        PlaylistTrack PlaylistTrack { get; }

        void Play();
    }
}