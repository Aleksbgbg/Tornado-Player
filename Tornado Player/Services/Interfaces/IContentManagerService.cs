namespace Tornado.Player.Services.Interfaces
{
    using System.Collections.Generic;

    using Tornado.Player.Models;

    internal interface IContentManagerService
    {
        Track AddTrack(string file);

        Track[] AddTracks(string directory);

        Playlist AddPlaylist(string name);

        Playlist AddPlaylist(string name, IEnumerable<Track> tracks);

        IEnumerable<Playlist> RetrievePlaylists();

        void SaveContent();
    }
}