namespace Tornado.Player.Services.Interfaces
{
    using System;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;

    internal interface IMusicPlayerService
    {
        event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        event EventHandler Paused;

        event EventHandler Played;

        event EventHandler<TrackChangedEventArgs> TrackChanged;

        Track Track { get; }

        bool IsPlaying { get; }

        TimeSpan Progress { get; set; }

        TimeSpan Duration { get; }

        double Volume { get; set; }

        bool Loop { get; set; }

        void Play();

        void Pause();

        void TogglePlayback();

        void SelectTrack(Track track);
    }
}