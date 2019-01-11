namespace Tornado.Player.Services.Interfaces
{
    using System.Collections.Generic;

    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;

    internal interface IContentManagerService
    {
        Track AddTrack(string file);

        Track[] AddTracks(string directory);

        Playlist AddPlaylist(string name);

        Playlist AddPlaylist(string name, IEnumerable<Track> tracks);

        bool DeletePlaylist(Playlist playlist);

        PlaylistTrack AddTrackToPlaylist(Playlist playlist, Track track);

        PlaylistTrack RemoveTrackFromPlaylist(Playlist playlist, Track track);

        void Favorite(Track track);

        void UnFavorite(Track track);

        IEnumerable<Playlist> RetrievePlaylists();

        IDictionary<ManagedPlaylist, Playlist> RetrieveManagedPlaylists();

        IEnumerable<Track> RetrieveTracks();

        void SaveContent();
    }
}