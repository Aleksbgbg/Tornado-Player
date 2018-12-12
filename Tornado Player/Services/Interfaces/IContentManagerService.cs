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

        PlaylistTrack AddTrackToPlaylist(Playlist playlist, Track track);

        PlaylistTrack RemoveTrackFromPlaylist(Playlist playlist, Track track);

        IEnumerable<Playlist> RetrievePlaylists();

        IEnumerable<Track> RetrieveTracks();

        void SaveContent();
    }
}