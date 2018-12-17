namespace Tornado.Player.Services.Interfaces
{
    using System;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;

    internal interface IMusicPlayerService
    {
        event EventHandler Played;

        event EventHandler Paused;

        event EventHandler MediaOpened;

        event EventHandler MediaEnded;

        event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        event EventHandler<TrackChangedEventArgs> TrackChanged;

        Track Track { get; }

        bool IsPlaying { get; }

        TimeSpan Progress { get; set; }

        TimeSpan Duration { get; }

        double Volume { get; set; }

        void Play();

        void Pause();

        void Stop();

        void TogglePlayback();

        void SelectTrack(Track track);
    }
}