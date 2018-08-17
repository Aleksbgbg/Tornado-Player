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

        event EventHandler Paused;

        event EventHandler Played;

        Track[] Tracks { get; }

        bool Loop { get; set; }

        TimeSpan Progress { get; set; }

        double Volume { get; set; }

        void Previous();

        void Next();

        void Play();

        void Pause();

        void TogglePlayback();

        void SelectTrack(int index);
    }
}