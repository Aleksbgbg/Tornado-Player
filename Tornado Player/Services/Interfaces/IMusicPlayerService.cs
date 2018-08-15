namespace Tornado.Player.Services.Interfaces
{
    using System;

    using Tornado.Player.EventArgs;
    using Tornado.Player.Models;

    internal interface IMusicPlayerService
    {
        event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        event EventHandler<TrackChangedEventArgs> TrackChanged;

        event EventHandler<PlaylistLoadedEventArgs> PlaylistLoaded;

        Track[] Tracks { get; }

        TimeSpan Progress { get; set; }

        void Play();

        void Pause();

        void TogglePlayback();

        void SelectTrack(int index);
    }
}