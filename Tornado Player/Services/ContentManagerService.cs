namespace Tornado.Player.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Tornado.Player.Models;
    using Tornado.Player.Models.Player;
    using Tornado.Player.Services.Interfaces;

    internal class ContentManagerService : IContentManagerService
    {
        private readonly IDataService _dataService;

        private readonly ISnowflakeService _snowflakeService;

        private readonly Dictionary<ulong, Track> _trackRepository;

        private readonly List<Playlist> _playlists;

        private readonly Dictionary<ManagedPlaylist, Playlist> _managedPlaylists;

        public ContentManagerService(IDataService dataService, ISnowflakeService snowflakeService)
        {
            _dataService = dataService;
            _snowflakeService = snowflakeService;

            Track[] tracks = dataService.Load(Constants.DataStoreNames.Tracks, () => new Track[0]);
            _trackRepository = tracks.ToDictionary(track => track.Id, track => track);

            _playlists = _dataService.Load(Constants.DataStoreNames.Playlists, () => new List<Playlist>());

            _managedPlaylists = dataService.Load(Constants.DataStoreNames.ManagedPlaylists, () => Enumerable.Empty<Playlist>())
                                           .ToDictionary(playlist =>
                                                         {
                                                             Enum.TryParse(playlist.Name, out ManagedPlaylist result);
                                                             return result;
                                                         },
                                                         playlist => playlist);

            if (!_managedPlaylists.ContainsKey(ManagedPlaylist.Favorites))
            {
                _managedPlaylists[ManagedPlaylist.Favorites] = new Playlist
                (
                    _snowflakeService.GenerateSnowflake(),
                    ManagedPlaylist.Favorites.ToString(),
                    false,
                    0,
                    TimeSpan.Zero,
                    _trackRepository.Values.Where(track => track.IsFavorite)
                                    .Select(track => new PlaylistTrack(0, track))
                );
            }

            foreach (Playlist playlist in _playlists)
            {
                playlist.Load(_trackRepository);
            }
        }

        public Track AddTrack(string file)
        {
            Track track = new Track(_snowflakeService.GenerateSnowflake(), file);

            _trackRepository[track.Id] = track;

            return track;
        }

        public Track[] AddTracks(string directory)
        {
            string[] files = Directory.GetFiles(directory);

            Track[] newTracks = new Track[files.Length];

            for (int index = 0; index < files.Length; ++index)
            {
                newTracks[index] = AddTrack(files[index]);
            }

            return newTracks;
        }

        public Playlist AddPlaylist(string name)
        {
            return AddPlaylist(name, Enumerable.Empty<Track>());
        }

        public Playlist AddPlaylist(string name, IEnumerable<Track> tracks)
        {
            Playlist playlist = new Playlist(_snowflakeService.GenerateSnowflake(), name, 0, tracks);

            _playlists.Add(playlist);

            return playlist;
        }

        public bool DeletePlaylist(Playlist playlist)
        {
            return _playlists.Remove(playlist);
        }

        public PlaylistTrack AddTrackToPlaylist(Playlist playlist, Track track)
        {
            return playlist.AddTrack(track.Id, _trackRepository);
        }

        public PlaylistTrack RemoveTrackFromPlaylist(Playlist playlist, Track track)
        {
            return playlist.RemoveTrack(track.Id);
        }

        public void Favorite(Track track)
        {
            track.IsFavorite = true;
            AddTrackToPlaylist(_managedPlaylists[ManagedPlaylist.Favorites], track);
        }

        public void UnFavorite(Track track)
        {
            track.IsFavorite = false;
            RemoveTrackFromPlaylist(_managedPlaylists[ManagedPlaylist.Favorites], track);
        }

        public IEnumerable<Playlist> RetrievePlaylists()
        {
            return _playlists;
        }

        public IDictionary<ManagedPlaylist, Playlist> RetrieveManagedPlaylists()
        {
            return _managedPlaylists;
        }

        public IEnumerable<Track> RetrieveTracks()
        {
            return _trackRepository.Values;
        }

        public void SaveContent()
        {
            _dataService.Save(Constants.DataStoreNames.Tracks, _trackRepository.Values);
            _dataService.Save(Constants.DataStoreNames.Playlists, _playlists);
        }
    }
}