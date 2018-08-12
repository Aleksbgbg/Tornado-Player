namespace Tornado.Player.Services.Interfaces
{
    using System;

    using Tornado.Player.EventArgs;

    internal interface IMusicPlayerService
    {
        event EventHandler<TrackChangedEventArgs> TrackChanged;

        event EventHandler<PlaylistLoadedEventArgs> PlaylistLoaded;
    }
}